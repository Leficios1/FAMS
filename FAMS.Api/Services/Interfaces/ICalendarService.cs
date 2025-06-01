using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;

namespace FAMS.Api.Services.Interfaces
{
    public interface ICalendarService
    {
        Task<object> ViewTrainingCalendar(int userid, string type, DateTime? time);
        Task<DayCalendarDto> GetDayCalenderDto(int userid, DateTime time);
        Task<CalendarDto> GetWeekCalendarDto(int userid, DateTime time);
        Task<StatusCodeResponse<CalenderUpdateDTO>> UpdateCalenderByClassID(CalenderUpdateDTO calenderDTO);
    }
}
