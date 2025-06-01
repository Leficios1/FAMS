using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Net;

namespace FAMS.Api.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IBaseRepository<CalendarClass> _calendarClassRepository;
        private readonly FamsContext _context;
        private readonly IClassService _classService;

        public CalendarService(IBaseRepository<CalendarClass> calendarClassRepository,
            FamsContext context, IClassService classService)
        {
            _context = context;
            _calendarClassRepository = calendarClassRepository;
            _classService = classService;

        }
        public async Task<object> ViewTrainingCalendar(int userid, string type, DateTime? time)
        {
            switch (type.ToLower())
            {
                case "week":
                    var result = new CalendarDto();
                    result = await GetWeekCalendarDto(userid, time ?? DateTime.Now);
                    return result;
                case "day":
                    var dayResult = new DayCalendarDto();
                    dayResult = await GetDayCalenderDto(userid, time ?? DateTime.Now);
                    return dayResult;
                default:
                    throw new NotImplementedException();
            }
        }
        public async Task<DayCalendarDto> GetDayCalenderDto(int userid, DateTime time)
        {
            var result = new DayCalendarDto();
            result.DayofWeek = time.DayOfWeek.ToString();
            var classes = await _classService.GetClassByUserId(userid);
            var list = new List<SlotCalendarDto>();
            foreach (var c in classes)
            {
                var slots = await _calendarClassRepository.Get().Where(s => s.DateAndTimeStudy.Date == time.Date && s.ClassId == c.Id).ToListAsync();
                foreach (var slot in slots)
                {
                    var clas = await _classService.GetClassById(slot.Id);
                    var s = new SlotCalendarDto();
                    s.ClassName = clas.ClassName;
                    s.Status = clas.Status;
                    s.TrainerName = await _context.ClassUsers.Where(x => x.ClassId == slot.Id && x.User.PermissionId.ToUpper().Equals("TR".ToUpper())).Select(x => x.User.Name).FirstOrDefaultAsync();
                    s.AdminName = await _context.ClassUsers.Where(x => x.ClassId == slot.Id && x.User.PermissionId.ToUpper().Equals("AD".ToUpper())).Select(x => x.User.Name).FirstOrDefaultAsync();
                    s.TrainingProgramName = await _context.Classes.Where(x => x.TrainingProgramCode.Equals(clas.TrainingProgramCode)).Select(x => x.TrainingProgram.Name).FirstOrDefaultAsync();
                    s.CurentSlot = (await _context.CalendarClasses.Where(x => x.DateAndTimeStudy.Date <= time.Date && x.ClassId == slot.ClassId).ToArrayAsync()).Length;
                    s.TotalSlot = (await _context.CalendarClasses.Where(x => x.ClassId == slot.ClassId).ToArrayAsync()).Length;
                    s.Time = slot.DateAndTimeStudy;
                    DateTime start = clas.ClassTimeStart.Value;
                    DateTime end = clas.ClassTimeEnd.Value;
                    s.ClassTime = $"{start.ToString("HH:mm")}-{end.ToString("HH:mm")}";
                    list.Add(s);
                }
            }
            result.slotCalendarDTOs = list.ToArray();
            return result;
        }
        public async Task<CalendarDto> GetWeekCalendarDto(int userid, DateTime time)
        {
            var result = new CalendarDto();
            DateTime startOfWeek = time.AddDays(-(int)time.DayOfWeek);
            var listDay = new List<DayCalendarDto>();
            for (DateTime i = startOfWeek; i < startOfWeek.AddDays(7); i = i.AddDays(1))
            {
                var Day = await GetDayCalenderDto(userid, i);
                listDay.Add(Day);
            }
            result.dayCalendarDTOs = listDay.ToArray();
            return result;
        }

        public async Task<StatusCodeResponse<CalenderUpdateDTO>> UpdateCalenderByClassID(CalenderUpdateDTO calenderDTO)
        {
            var response = new StatusCodeResponse<CalenderUpdateDTO>();
            response.Data = calenderDTO;
            using (var trasaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (calenderDTO.ClassID == null)
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.Errormessge = EMS.EM82;
                        response.Data = null;
                        return response;
                    }
                    else if (calenderDTO.DateAndTimeStudy == null)
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.Errormessge = EMS.EM83;
                        response.Data = null;
                        return response;
                    }
                    else
                    {
                        var existingCalendars = await _context.CalendarClasses.Where(cc => cc.ClassId == calenderDTO.ClassID).ToListAsync();
                        if(existingCalendars == null)
                        {
                            response.statusCode = HttpStatusCode.NotFound;
                            response.Errormessge = EMS.EM84;
                            response.Data = null;
                            return response;
                        }
                        _context.CalendarClasses.RemoveRange(existingCalendars);
                        await _context.SaveChangesAsync();
                        foreach (var calender in response.Data.DateAndTimeStudy)
                        {
                            var newCalender = new CalendarClass
                            {
                                ClassId = calenderDTO.ClassID,
                                DateAndTimeStudy = calender,
                            };
                            _context.CalendarClasses.Add(newCalender);
                        }
                        await _context.SaveChangesAsync();
                        await trasaction.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    await trasaction.RollbackAsync();
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.Errormessge = EMS.EM70;
                    response.Data = null;
                    return response;
                }
            }
            response.statusCode = HttpStatusCode.OK;
            response.Errormessge = "Update SuccessFul";
            return response;
        }
    }
}
