using AutoMapper;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using FAMS.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.WebSockets;
using System.Web.Helpers;

namespace FAMS.Api.Services
{
    public class ClassService : IClassService
    {
        private readonly IBaseRepository<Class> _class;
        private readonly FamsContext _context;
        private readonly IBaseRepository<ClassUser> _classUser;
        private readonly IBaseRepository<TrainingProgram> _trainingProgram;
        private readonly IBaseRepository<TrainingProgramSyllabus> _trainingProgramSyllabusRepo;
        private readonly IBaseRepository<CalendarClass> _calendarRepo;
        private readonly IBaseRepository<AssessmentSchemeDTO> _assessmentSchemeRepo;
        public readonly IMapper _mapper;
        public readonly IBaseRepository<User> _userRepo;
        public readonly ISyllabusService _syllabusService;
        private readonly IBaseRepository<ClassTrainerUnit> _classTrainerUnitRepo;
        public ClassService(ISyllabusService syllabusService,
        IBaseRepository<Class> Class, FamsContext context, IBaseRepository<ClassUser> classUser, IBaseRepository<TrainingProgram> trainingProgram,
        IBaseRepository<TrainingProgramSyllabus> trainingProgramSyllabusRepo, IBaseRepository<CalendarClass> calendarRepo, IBaseRepository<AssessmentSchemeDTO> assessmentSchemeRepo,
        IMapper mapper, IBaseRepository<User> userRepo, IBaseRepository<ClassTrainerUnit> classTrainerUnitRepo)
        {
            _class = Class;
            _context = context;
            _classUser = classUser;
            _trainingProgram = trainingProgram;
            _trainingProgramSyllabusRepo = trainingProgramSyllabusRepo;
            _calendarRepo = calendarRepo;
            _assessmentSchemeRepo = assessmentSchemeRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _syllabusService = syllabusService;
            _classTrainerUnitRepo = classTrainerUnitRepo;
        }

        public async Task<IActionResult> ViewClassDetail(int id)
        {
            var resultClass = await _class.Get().Include(x => x.Admins).ThenInclude(x => x.User).Include(x => x.CalendarClasses).Include(x => x.TrainerUnits)
                .ThenInclude(x => x.Trainer).Include(x => x.TrainerUnits).ThenInclude(x => x.TrainingUnit).ThenInclude(x => x.Syllabus)
                .Include(x => x.TrainingProgram).ThenInclude(x => x.TrainingProgramSyllabuses).ThenInclude(x => x.Syllabus).ThenInclude(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (resultClass == null) return new BadRequestObjectResult(EMS.EM50 + id);

            var returnedClass = _mapper.Map<ViewDetailClassDTO>(resultClass);

            var durationByHours = resultClass?.TrainingProgram?.TrainingProgramSyllabuses?.SelectMany(x => x.Syllabus.TrainingUnits).SelectMany(x => x.TrainingContents).Sum(x => x.Duration);

            returnedClass.DurationByHour = (float)(durationByHours ?? 0);
            returnedClass.InfoTrainers = resultClass.TrainerUnits != null && resultClass.TrainerUnits.Any() ?
                                                 resultClass.TrainerUnits.Select(x => _mapper.Map<InfoTrainer>(x)).ToArray() : new InfoTrainer[] { };
            returnedClass.InfoAdmins = resultClass.Admins != null && resultClass.Admins.Any() ? resultClass.Admins.Select(x => x.User).Select(x => _mapper.Map<InfoUser>(x)).ToArray() : new InfoUser[] { };

            var FSUStrings = resultClass.FSU?.Split("-").Select(x => x.Trim()).ToArray() ?? new string[0];
            returnedClass.FSU = FSUStrings.Length > 0 ? FSUStrings[0] : Value.NoInformation;
            returnedClass.EmailFSU = FSUStrings.Length > 1 ? FSUStrings[1] : Value.NoInformation;
            var trainingProgram = resultClass.TrainingProgram;
            if (trainingProgram != null)
            {
                var syllabus = resultClass.TrainingProgram?.TrainingProgramSyllabuses?.Select(x => _mapper.Map<SyllabusCard>(x));
                var program = _mapper.Map<TrainingProgramCardInClass>(trainingProgram);
                program.Syllabuses = syllabus != null ? syllabus.ToArray() : new SyllabusCard[] { };
                program.DurationByHour = (float)(durationByHours ?? 0);
                returnedClass.TrainingProgram = program;
            }
            return new OkObjectResult(returnedClass);
        }

        public async Task<IActionResult> SearchClassOnList(int? PageNumber = null, int? PageSize = null, string? searchString = null
                                                , string? locations = null, string? startDate = null, string? endDate = null, string? attendees = null, string? status = null, string? typeClassTime = null, string? FSU = null, int? trainerId = null, string? sortBy = null, string? order = null)
        {

            var query = _class.Get();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                query = query.Where(x => x.ClassCode.ToLower().Contains(searchString) || x.ClassName.ToLower().Contains(searchString)
                                          || (x.Location != null && x.Location.ToLower().Contains(searchString)) || x.Status.ToLower().Contains(searchString) ||
                                           (x.FSU != null && x.FSU.ToLower().Contains(searchString) || x.CreatedBy.ToLower().Contains(searchString)));
            }
            if (!string.IsNullOrEmpty(typeClassTime))
            {
                var types = DivideString(typeClassTime);
                foreach (var type in types)
                {
                    IQueryable<Class> filteredQuery = null;
                    switch (type)
                    {
                        case OrderByConstant.ClassTime_Morning:
                            filteredQuery = query.Where(x => x.ClassTimeStart.HasValue &&
                                                      x.ClassTimeStart.Value.TimeOfDay >= TimeSpan.Parse(OrderByConstant.ClassTime_MorningBeginRange) &&
                                                      x.ClassTimeStart.Value.TimeOfDay < TimeSpan.Parse(OrderByConstant.ClassTime_MorningEndRange));
                            break;
                        case OrderByConstant.ClassTime_Noon:
                            filteredQuery = query.Where(x => x.CalendarClasses.Any(c =>
                                                      c.DateAndTimeStudy.TimeOfDay >= TimeSpan.Parse(OrderByConstant.ClassTime_NoonBeginRange) &&
                                                      c.DateAndTimeStudy.TimeOfDay < TimeSpan.Parse(OrderByConstant.ClassTime_NoonEndRange)));
                            break;
                        case OrderByConstant.ClassTime_Afternoon:
                            filteredQuery = query.Where(x => x.CalendarClasses.Any(c =>
                                                      c.DateAndTimeStudy.TimeOfDay >= TimeSpan.Parse(OrderByConstant.ClassTime_AfternoonBeginRange) &&
                                                      c.DateAndTimeStudy.TimeOfDay < TimeSpan.Parse(OrderByConstant.ClassTime_AfternoonEndRange)));
                            break;
                        case OrderByConstant.ClassTime_Evening:
                            filteredQuery = query.Where(x => x.CalendarClasses.Any(c =>
                                                      c.DateAndTimeStudy.TimeOfDay >= TimeSpan.Parse(OrderByConstant.ClassTime_EveningBeginRange) &&
                                                      c.DateAndTimeStudy.TimeOfDay < TimeSpan.Parse(OrderByConstant.ClassTime_EveningEndRange)));
                            break;
                    }
                    if (filteredQuery != null) query = query.Union(filteredQuery);
                }

            }
            if (!string.IsNullOrEmpty(attendees))
            {
                var attendee = DivideString(locations).Select(x => x.ToLower().Trim());
                query = query.Where(x => x.Attendee != null && attendee.Contains(x.Attendee.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(locations))
            {
                var location = DivideString(locations).Select(x => x.ToLower().Trim());
                query = query.Where(x => x.Location != null && location.Contains(x.Location.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(startDate))
            {
                var valueStartDate = DateTime.Parse(startDate).Date;
                query = query.Where(x => x.CreatedDate.Date >= valueStartDate);
            }


            if (!string.IsNullOrEmpty(endDate))
            {
                var valueEndDate = DateTime.Parse(endDate).Date;
                query = query.Where(x => x.CreatedDate.Date <= valueEndDate);
            }

            query = !string.IsNullOrEmpty(FSU) ? query.Where(x => x.FSU != null && x.FSU.ToLower().Contains(FSU.ToLower().Trim())) : query;


            if (!string.IsNullOrEmpty(status))
            {
                var statuses = DivideString(status);
                query = query.Where(x => statuses.Contains(x.Status.ToLower()));
            }

            query = trainerId.HasValue ? query.Where(x => (x.TrainerUnits != null && x.TrainerUnits.Any()
                                                      && x.TrainerUnits.Select(x => x.Trainer).Select(x => x.Id).Contains(trainerId.Value))) : query;

            if (sortBy != null)
                switch (sortBy.ToLower())
                {
                    case OrderByConstant.SortBy_Id:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                        break;
                    case OrderByConstant.SortBy_Code:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.ClassCode) : query.OrderBy(x => x.ClassCode);
                        break;
                    case OrderByConstant.SortBy_Name:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.ClassName) : query.OrderBy(x => x.ClassName);
                        break;
                    case OrderByConstant.SortBy_CreatedBy:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy);
                        break;

                    case OrderByConstant.SortBy_ModifiedDate:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.ModifiedDate) : query.OrderBy(x => x.ModifiedDate);
                        break;
                    case OrderByConstant.SortBy_Duration:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.Duration) : query.OrderBy(x => x.Duration);
                        break;

                    case OrderByConstant.SortBy_Attendee:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.Attendee) : query.OrderBy(x => x.Attendee);
                        break;
                    case OrderByConstant.SortBy_Location:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.Location) : query.OrderBy(x => x.Location);
                        break;
                    case OrderByConstant.SortBy_FSU:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.FSU) : query.OrderBy(x => x.FSU);
                        break;
                    case OrderByConstant.SortBy_Status:
                        query = !string.IsNullOrEmpty(order) && order.ToLower().Equals(OrderByConstant.OrderByDESC) ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status);
                        break;

                }
            if (!PageSize.HasValue || PageSize < 1)
            {
                PageSize = 10;
            }

            if (!PageNumber.HasValue || PageNumber < 1)
            {
                PageNumber = 1;
            }
            var resultList = await query.ToListAsync();
            var returnedList = resultList.Select(x =>
            {
                var result = _mapper.Map<ViewListClassDTO>(x);
                result.FSU = x.FSU.Split("-").First();
                return result;
            });
            var totalPages = resultList.Count % PageSize == 0 ? (resultList.Count) / PageSize : (resultList.Count / PageSize) + 1;
            returnedList = returnedList.Skip((int)((PageNumber - 1) * PageSize)).Take((int)PageSize).ToList();

            return new OkObjectResult(new ViewListResponse()
            {
                TotalPage = (int)totalPages,
                PageNumber = (int)PageNumber,
                PageSize = (int)PageSize,
                List = returnedList.ToArray()
            });
        }
        private IEnumerable<string> DivideString(string name)
        {
            var names = name.Split(',').Select(x => x.Trim().ToLower());
            return names;
        }

        private Boolean checkUpdate(string status)
        {
            if (status == "Planning" || status == "Scheduled")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IActionResult> UpdateClass01(UpdateClass01RequestDto classUpdateRequest)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (classUpdateRequest.AdminId != null && classUpdateRequest.AdminId.Any())
                    {
                        foreach (var userId in classUpdateRequest.AdminId)
                        {
                            var checkUserId = await _userRepo.Get().AnyAsync(x => x.Id == userId);
                            if (!checkUserId) return new BadRequestObjectResult(EMS.EM51 + userId);
                        }
                    }

                    var @class = await _class.Get().Include(c => c.CalendarClasses).Include(x => x.TrainerUnits).FirstOrDefaultAsync(c => c.Id == classUpdateRequest.Id);
                    if (@class == null) return new BadRequestObjectResult(EMS.EM50 + classUpdateRequest.Id);

                    if (checkUpdate(@class.Status) == false)
                    {
                        return new BadRequestObjectResult(EMS.EM62);
                    }

                    if (!classUpdateRequest.DateAndTimeStudy.IsNullOrEmpty())
                    {
                        var existingDate = @class.CalendarClasses.Where(x => x.ClassId == classUpdateRequest.Id).ToList();
                        @class.CalendarClasses.Clear();

                        foreach (var date in classUpdateRequest.DateAndTimeStudy)
                        {
                            var calendarClass = new CalendarClass
                            {
                                ClassId = @class.Id,
                                DateAndTimeStudy = date
                            };
                            @class.CalendarClasses.Add(calendarClass);
                            await _calendarRepo.AddAsync(calendarClass);
                        }
                    }
                    _mapper.Map(classUpdateRequest, @class);

                    TimeSpan dateDif = classUpdateRequest.EndDate - classUpdateRequest.StartDate;

                    if (dateDif.Days < @class.CalendarClasses.Select(x => x.Id).Count())
                    {
                        await transaction.RollbackAsync();
                        return new BadRequestObjectResult(EMS.EM61);
                    }
                    else
                    {
                        @class.Duration = @class.CalendarClasses.Count();
                    }

                    if (classUpdateRequest.AdminId != null && classUpdateRequest.AdminId.Any())
                    {
                        var existingAdmins = await _classUser.Get().Where(cu => cu.ClassId == classUpdateRequest.Id).ToListAsync();
                        _context.RemoveRange(existingAdmins);
                        await _context.SaveChangesAsync();

                        foreach (var userId in classUpdateRequest.AdminId)
                        {
                            _context.Add(new ClassUser
                            {
                                UserId = userId,
                                ClassId = classUpdateRequest.Id
                            });
                        }



                    }
                    if (classUpdateRequest.TrainingProgramCode.HasValue && classUpdateRequest.TrainingProgramCode.Value > 0 && classUpdateRequest.TrainingProgramCode == @class.TrainingProgramCode)
                    {

                        if (classUpdateRequest.Trainers != null && classUpdateRequest.Trainers.Any())
                        {
                            foreach (var trainer in classUpdateRequest.Trainers)
                            {
                                var oldTrainer = @class.TrainerUnits.FirstOrDefault(x => x.UnitCode == trainer.UnitCode);
                                if (oldTrainer != null)
                                {
                                    oldTrainer.Location = trainer.Location;
                                    oldTrainer.UnitCode = trainer.UnitCode;
                                    oldTrainer.TrainerId = trainer.TrainerId;
                                    _context.Update(oldTrainer);
                                }
                                else
                                {
                                    await _context.AddAsync(new ClassTrainerUnit
                                    {
                                        ClassId = @class.Id,
                                        Location = trainer.Location,
                                        UnitCode = trainer.UnitCode,
                                        TrainerId = trainer.TrainerId
                                    });
                                }
                            }


                        }
                    }
                    else if (classUpdateRequest.TrainingProgramCode.HasValue && classUpdateRequest.TrainingProgramCode.Value > 0 && classUpdateRequest.TrainingProgramCode != @class.TrainingProgramCode)
                    {
                        @class.TrainingProgramCode = (int)classUpdateRequest.TrainingProgramCode;
                        var classTrainerUnit = await _context.ClassTrainerUnits.Where(x => x.ClassId == @class.Id).ToListAsync();
                        _context.RemoveRange(classTrainerUnit);
                        foreach (var trainer in classUpdateRequest.Trainers)
                        {
                            await _context.AddAsync(new ClassTrainerUnit
                            {
                                ClassId = @class.Id,
                                Location = trainer.Location,
                                UnitCode = trainer.UnitCode,
                                TrainerId = trainer.TrainerId
                            });
                        }


                    }



                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var result = await ViewClassDetail(classUpdateRequest.Id);
                    return result;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"{e}");
                }
            }
        }

        public async Task<IEnumerable<ViewListClassDTO>> GetByTrainingProgramCode(int trainingProgramCode)
        {
            var resultList = await _class.Get().Where(x => x.TrainingProgramCode == trainingProgramCode).Include(x => x.Admins).ThenInclude(x => x.User).ThenInclude(x => x.UserPermission).ToListAsync();
            var returnedList = resultList.Select(x => _mapper.Map<ViewListClassDTO>(x));
            return returnedList;
        }
        private async Task<List<string>> ValidateCreateClassDto(CreateClassDto createClassDto)
        {
            var user = _context.Users.ToList();
            var validationErrors = new List<string>();
            if (createClassDto == null)
            {
                validationErrors.Add("CreateClassDto is null");
                return validationErrors;
            }
            foreach (var code in createClassDto.AdminId)
            {
                var search = user.Where(x => x.Id == code && x.PermissionId == "AD");
                if (search.IsNullOrEmpty())
                {
                    validationErrors.Add("AdminId does not exist");
                }
            }
            foreach (var code in createClassDto.Trainers.Select(x => x.TrainerId))
            {
                var search = user.Where(x => x.Id == code && x.PermissionId == "TR");
                if (search.IsNullOrEmpty())
                {
                    validationErrors.Add("TrainerId does not exist");
                }
            }
            var classCodeExist = _context.Classes.Select(c => c.ClassCode);
            if (classCodeExist.Contains(createClassDto.ClassCode))
            {
                validationErrors.Add("ClassCode is existed");
            }
            var allowedStatuses = new List<string> { "Planning", "Scheduled", "Opening", "Closed" };
            if (!allowedStatuses.Contains(createClassDto.Status))
            {
                validationErrors.Add("Invalid status");
            }
            if (createClassDto.Calendar.Length >= createClassDto.Duration)
            {
                validationErrors.Add(EMS.EM61);
            }
            var trainingProgram = await _context.TrainingPrograms
                .Include(tp => tp.TrainingProgramSyllabuses)
                .FirstOrDefaultAsync(tp => tp.TrainingProgramCode == createClassDto.TrainingProgramCode);
            if (trainingProgram.Status != 1)
            {
                validationErrors.Add(EMS.EM53);
            }
            if (trainingProgram == null)
            {
                validationErrors.Add(EMS.EM48);
            }
            return validationErrors;
        }
        public async Task<IActionResult> CreateClass(CreateClassDto createClassDto)
        {
            var validationErrors = await ValidateCreateClassDto(createClassDto);

            if (validationErrors.Any())
            {
                return new BadRequestObjectResult(validationErrors);
            }
            try
            {
                var newClass = _mapper.Map<Class>(createClassDto);

                newClass.CreatedDate = DateTimeOffset.UtcNow;
                newClass.ModifiedBy = createClassDto.CreatedBy;
                newClass.ModifiedDate = DateTimeOffset.UtcNow;
                await _class.AddAsync(newClass);
                await _class.SaveChangesAsync();
                if (createClassDto.Trainers.Select(x => x.TrainerId) != null)
                {
                    foreach (var trainer in createClassDto.Trainers)
                    {
                        var newTrainer = new ClassTrainerUnit
                        {

                            TrainerId = trainer.TrainerId,
                            ClassId = newClass.Id,
                            UnitCode = trainer.UnitCode,
                            Location = trainer.Location
                        };
                        await _context.AddAsync(newTrainer);

                    }
                    await _context.SaveChangesAsync();
                }

                if (createClassDto.AdminId != null)
                {
                    foreach (var adminId in createClassDto.AdminId)
                    {
                        var classUser = new ClassUser

                        {
                            UserId = adminId,
                            ClassId = newClass.Id,
                            UserType = 2
                        };
                        await _classUser.AddAsync(classUser);
                    }
                    await _context.SaveChangesAsync();
                }
                if (createClassDto.Calendar != null)
                {
                    foreach (var calendarDate in createClassDto.Calendar)
                    {
                        var calendarClass = new CalendarClass
                        {
                            ClassId = newClass.Id,
                            DateAndTimeStudy = calendarDate
                        };
                        await _context.CalendarClasses.AddAsync(calendarClass);
                    }
                    await _context.SaveChangesAsync();
                }
                return new OkObjectResult(new { createClassDto, message = EMS.EM85 });
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }
        public async Task<IEnumerable<Class>> GetClassByUserId(int Id)
        {
            var result = await _class.Get().SelectMany(x => x.Admins).Where(x => x.UserId == Id).Select(x => x.Class).ToListAsync();
            return result;
        }
        public async Task<Class> GetClassById(int Id)
        {
            var result = await _class.Get().Where(x => x.Id == Id).SingleOrDefaultAsync();
            return result;
        }
        public async Task<bool> checkDeleteClass(int id)
        {
            var flag = await _class.Get().Where(x => x.Id == id).AnyAsync(x => x.Status != "Opening");
            return flag;
        }
        public async Task<StatusCodeResponse<bool>> deleteClass(int id)
        {
            if ((await checkDeleteClass(id)) == false) return new StatusCodeResponse<bool>()
            {
                statusCode = HttpStatusCode.BadRequest,
                Data = false,
                Errormessge = EMS.EM86,
            };
            var result = new StatusCodeResponse<bool>();
            var classDelete = await _class.Get().Include(x => x.CalendarClasses).Include(x => x.TrainerUnits).Include(x => x.Admins).FirstOrDefaultAsync(x => x.Id == id);
            if (classDelete != null)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    if (classDelete.CalendarClasses != null && classDelete.CalendarClasses.Any())
                        try
                        {

                            foreach (var c in classDelete.CalendarClasses)
                            {
                                _calendarRepo.Delete(c);
                                await _calendarRepo.SaveChangesAsync();
                            }
                            await transaction.CommitAsync();
                        }
                        catch (Exception e)
                        {
                            await transaction.RollbackAsync();
                            result.statusCode = HttpStatusCode.InternalServerError;
                            result.Data = false;
                            result.Errormessge = e.Message.ToString();
                            return result;
                        }
                    if (classDelete.Admins != null && classDelete.Admins.Any())
                        try
                        {

                            foreach (var c in classDelete.Admins)
                            {
                                _classUser.Delete(c);
                                await _classUser.SaveChangesAsync();
                            }
                            await transaction.CommitAsync();
                        }
                        catch (Exception e)
                        {
                            await transaction.RollbackAsync();
                            result.statusCode = HttpStatusCode.InternalServerError;
                            result.Data = false;
                            result.Errormessge = e.Message.ToString();
                            return result;
                        }
                    if (classDelete.TrainerUnits != null && classDelete.TrainerUnits.Any())
                        try
                        {

                            foreach (var c in classDelete.TrainerUnits)
                            {
                                _classTrainerUnitRepo.Delete(c);
                                await _classTrainerUnitRepo.SaveChangesAsync();
                            }
                            await transaction.CommitAsync();
                        }

                        catch (Exception e)
                        {
                            await transaction.RollbackAsync();
                            result.statusCode = HttpStatusCode.InternalServerError;
                            result.Data = false;
                            result.Errormessge = e.Message.ToString();
                            return result;
                        }
                }
                _class.Delete(classDelete);
                await _class.SaveChangesAsync();
                result.statusCode = HttpStatusCode.OK;
                result.Data = true;
                result.Errormessge = "Successful";
            }
            else
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.Data = false;
                result.Errormessge = EMS.EM50;
            }
            return result;
        }
        public async Task<IActionResult> ChangeStatusClass(int classCode, int status)
        {
            var classes = await _class.GetById(classCode);
            if (classes == null) return new BadRequestObjectResult(EMS.EM50);
            classes.Status = ((ClassEnum)status).ToString();
            _class.Update(classes);
            await _class.SaveChangesAsync();
            return new OkObjectResult(new { ClassCode = classCode, Status = status });
        }
        public async Task<IActionResult> DuplicationClass(int id)
        {
            var originClass = await _class.Get()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (originClass == null)
            {
                return new NotFoundObjectResult("Not found Class");
            }
            var duplicate = _mapper.Map(originClass, new Class());
                duplicate.Id = new int();
                duplicate.CreatedDate = DateTime.Now;
                duplicate.ModifiedDate = DateTime.Now;
            var existingClasses = await _class.Get()
        .Where(x => x.ClassCode.Contains(originClass.ClassCode))
        .ToListAsync();
            if (existingClasses.Count > 0)
            {
                int classCodeNumber = 1;
                string newClassCode = originClass.ClassCode;
                while (existingClasses.Any(x => x.ClassCode == newClassCode))
                {
                    classCodeNumber++;
                    newClassCode = originClass.ClassCode + " " + classCodeNumber;
                }
                duplicate.ClassCode = newClassCode;
            }
            await _class.AddAsync(duplicate);
            await _context.SaveChangesAsync();
            var viewResult = await _class.Get()
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
            if (viewResult == null)
            {
                throw new Exception("Unable to duplicate class");
            }
            return new OkObjectResult(viewResult);
        }
    }
}
