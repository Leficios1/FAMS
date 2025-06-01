using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Writers;
using System.Globalization;

namespace FAMS.Api.Configurations.Mapper
{
    public class MappingEntities : Profile
    {
        public MappingEntities()
        {
            CreateMapUser();
            CreateMapSyllabus();
            CreateMapUserPermission();
            CreateMapClass();
            CreateMapTrainingProgram();
            CreateMapDeliveryType();
            CreateMapTrainingUnit();
            CreateMapTrainingContent();
            CreateMapTrainingProgramSyllabus();
            CreateMapMaterial();
            CreateMapAssessmentScheme();
            CreateMapClassTrainerUnit();
        }

        private void CreateMapUser()
        {
            CreateMap<UserRequestDto, User>().ReverseMap();
            CreateMap<User, UserResponseDto>().ForMember(x=>x.AvatarUrl,opt=>opt.MapFrom(src=>src.AvatarUrl.Replace("%2F","/")))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(src => src.UserPermission.RoleName));
            CreateMap<AuthDto, User>()
                .ForMember(user => user.Email, auth => auth.MapFrom(src => src.Email))
                .ForMember(user => user.Password, auth => auth.MapFrom(src => src.Password))
 
                .ReverseMap();
            CreateMap<CreateUserDTO, User>()
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth)))
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(x => x.CreatedBy))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => DateTime.UtcNow));
            CreateMap<UpdateUserDTO, User>()
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .ForMember(x => x.Id, opt => opt.DoNotUseDestinationValue());
            CreateMap<User, InfoUser>().ForMember(x=>x.AvatarUrl, opt=>opt.MapFrom(src=> src.AvatarUrl.Replace("%2F", "/")));
        }

        private void CreateMapSyllabus()
        {
            //CreateMap<SyllabusDto, Syllabus>().ForMember(x => x.UserId, y => y.MapFrom(src => src.UserId)).ReverseMap();

            CreateMap<SyllabusDto, Syllabus>().ReverseMap();
            CreateMap<Syllabus, ViewListSyllabusDTO>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString(Value.DateFormat)))
                .ForMember(dest => dest.OutputStandards, opt => opt.MapFrom(src => src.SyllabusObjectives != null && src.SyllabusObjectives.Any() ? src.SyllabusObjectives.Select(obj => obj.ObjectiveCode).ToArray() : new string[] { }))
                .ForMember(dest => dest.DurationByDay, opt => opt.MapFrom(src => src.TrainingUnits != null && src.TrainingUnits.Any() ? src.TrainingUnits.Max(x => x.DayNumber) : 0));
            CreateMap<Syllabus, ViewDetailSyllabusDto>()
                .ForMember(x => x.Level, opt => opt.MapFrom(src => src.Priority))
                .ForMember(x => x.OutpuStandards, opt => opt.MapFrom(src => src.SyllabusObjectives != null && src.SyllabusObjectives.Any() ? src.SyllabusObjectives.Select(x => x.ObjectiveCode).ToArray() : new string[] { }))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate.ToString(Value.DateFormat)))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate.HasValue ? x.ModifiedDate.Value.ToString(Value.DateFormat) : Value.NoInformation));
            CreateMap<UpdateSyllabusRequest, Syllabus>()
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(x => x.Priority, opt => opt.MapFrom(x => x.Level));
            CreateMap<CreatedSyllabusOutlineDto, StatusCodeResponse<CreatedSyllabusOutlineDto>>()
                .ForMember(dest => dest.statusCode, opt => opt.Ignore())
                .ForMember(dest => dest.Errormessge, opt => opt.Ignore())
                .ReverseMap();
        }

        private void CreateMapUserPermission()
        {
            CreateMap<UpdatePermissionDto, UserPermission>().ReverseMap();
            CreateMap<UserPermission, UserPermissionResponseDto>().ReverseMap();
        }

        private void CreateMapClass()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Class, ViewListClassDTO>()
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.Date.ToString(Value.DateFormat)))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.Date.ToString(Value.DateFormat)))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate.ToString(Value.DateFormat)))
                .ForMember(x => x.Trainers, opt => opt.MapFrom(x => x.Admins != null && x.Admins.Any() ? x.Admins.Select(x => x.User).Where(x => x.PermissionId == "TR").Select(x => x.Name) : new string[] { }));
            CreateMap<Class, ViewDetailClassDTO>()
                .ForMember(dest => dest.ClassTime, opt => opt.MapFrom(src => (src.ClassTimeStart != null ? src.ClassTimeStart.Value.ToString(Value.HourFormat) : "00:00") + "-" + (src.ClassTimeEnd != null ? src.ClassTimeEnd.Value.ToString(Value.HourFormat) : "00:00")))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.DateTime.ToString(Value.DateFormat)))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.DateTime.ToString(Value.DateFormat)))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.DateTime.ToString(Value.DateFormat)))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate.ToString(Value.DateFormat)))
                .ForMember(x => x.CalendarStudy, opt => opt.MapFrom(src => src.CalendarClasses != null && src.CalendarClasses.Any() ? src.CalendarClasses.Select(x => x.DateAndTimeStudy.ToString("MM/dd/yyyy")).ToArray() : new string[] { }))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(x => x.Duration));
            CreateMap<UpdateClass01RequestDto, Class>()
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(src => src.Id, opt => opt.DoNotUseDestinationValue())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateClassDto, Class>().ReverseMap();
        }

        private void CreateMapTrainingProgram()
        {
            CreateMap<CreateTrainingProgramDTO, TrainingProgram>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => DateTimeOffset.Now))
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(x => x.CreatedBy))
                .ForMember(x => x.StartTime, opt => opt.MapFrom(x => x.StartTime != null ? DateTimeOffset.Parse(x.StartTime) : DateTimeOffset.Now.AddDays(14)));
            CreateMap<TrainingProgramDtoRequest, TrainingProgram>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramCardInClass>()
                .ForMember(x => x.TrainingProgramName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate.Value.ToString(Value.DateFormat)))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(src => (int)src.Duration));
            CreateMap<TrainingProgram, ViewDetailTrainingProgramDTO>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.DateTime.ToString(Value.DateFormat)))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate.HasValue ? src.ModifiedDate.Value.ToString(Value.DateFormat) : Value.NoInformation))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(src => src.Duration))
                .ForMember(x => x.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString(Value.DateFormat)))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(src => src.Duration));
            CreateMap<TrainingProgram, ViewListTrainingProgramDTO>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.DateTime.ToString(Value.DateFormat)))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(x => x.Duration))
                .ForMember(x => x.DurationByHour, opt => opt.MapFrom(x => x.TrainingProgramSyllabuses != null ? x.TrainingProgramSyllabuses.SelectMany(x => x.Syllabus.TrainingUnits).SelectMany(x => x.TrainingContents).Sum(x => x.Duration) : 0));
            CreateMap<TrainingProgramDtoRequest, TrainingProgram>().ReverseMap();
        }

        private void CreateMapDeliveryType()
        {
            CreateMap<DeliveryType, CreateDeliveryTypeDto>().ReverseMap();
            CreateMap<DeliveryType, UpdateDeliveryTypeDto>().ReverseMap();
        }

        private void CreateMapTrainingUnit()
        {
            //CreateMap<TrainingUnitDto, TrainingUnit>().ReverseMap();

            CreateMap<TrainingUnitDto, TrainingUnit>()
                .ForMember(dest => dest.TrainingContents, opt => opt.MapFrom(src => src.TrainingContents))
                .ReverseMap();
            CreateMap<TrainingUnit, ViewOutlineSyllabus>();
            CreateMap<TrainingUnit, TrainingUnitCard>()
                .ForMember(x => x.DurationByHour, opt => opt.MapFrom(src => src.TrainingContents != null && src.TrainingContents.Any() ? src.TrainingContents.Sum(x => x.Duration) : 0));
            CreateMap<UpdateTrainingUnitDTO, TrainingUnit>().ForMember(x => x.UnitCode, opt => opt.NullSubstitute(0)).ForMember(x => x.TrainingContents, opt => opt.DoNotUseDestinationValue());
            CreateMap<TrainingUnitOutline, TrainingUnitDto>();
        }

        private void CreateMapTrainingContent()
        {
            CreateMap<TrainingContentDto, TrainingContent>()
                .ForMember(dest => dest.Materials, opt => opt.MapFrom(src => src.Materials))
                .ReverseMap();
            CreateMap<TrainingContent, TrainingContentCard>();
            CreateMap<UpdateTrainingContentDTO, TrainingContent>()
                .ForMember(x => x.Id, opt => opt.NullSubstitute(0))
                .ForMember(x => x.Materials, opt => opt.DoNotUseDestinationValue()).ReverseMap();
            CreateMap<Material, MaterialDto>();
        }

        private void CreateMapTrainingProgramSyllabus()
        {
            CreateMap<CreateTrainingProgramSyllabusDTO, TrainingProgramSyllabus>();
            CreateMap<TrainingProgramSyllabus, SyllabusCard>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.Id : 0))
                .ForMember(x => x.SyllabusName, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.SyllabusName : Value.NoInformation))
                .ForMember(x => x.SyllabusCode, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.SyllabusCode : Value.NoInformation))
                .ForMember(x => x.Version, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.Version : Value.NoInformation))
                .ForMember(x => x.PublishStatus, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.PublishStatus : 0))
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(x => x.Syllabus != null ? x.Syllabus.ModifiedBy : Value.NoInformation))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.Syllabus != null && x.Syllabus.ModifiedDate.HasValue ? x.Syllabus.ModifiedDate.Value.ToString(Value.DateFormat) : Value.NoInformation))
                .ForMember(x => x.DurationByDay, opt => opt.MapFrom(x => x.Syllabus != null && x.Syllabus.TrainingUnits != null && x.Syllabus.TrainingUnits.Any() ? x.Syllabus.TrainingUnits.Max(x => x.DayNumber) : 0))
                .ForMember(x => x.DurationByHour, opt => opt.MapFrom(x => x.Syllabus != null && x.Syllabus.TrainingUnits != null && x.Syllabus.TrainingUnits.SelectMany(x => x.TrainingContents).Any() ? x.Syllabus.TrainingUnits.SelectMany(x => x.TrainingContents).Sum(x => x.Duration) : 0));
        }

        private void CreateMapMaterial()
        {
            CreateMap<CMaterialDto, Material>().ReverseMap();
            CreateMap<CreateMaterialDTO, Material>().ReverseMap();
            CreateMap<Material, MaterialDto>().ReverseMap();
            CreateMap<string, Material>()
                .ForMember(x => x.Url, opt => opt.MapFrom(x => x))
                .ForMember(x => x.Id, opt => opt.NullSubstitute(0));
        }

        private void CreateMapAssessmentScheme()
        {
            CreateMap<AssessmentSchemeRequest, AssessmentScheme>().ReverseMap();
            CreateMap<AssessmentScheme, AssessmentSchemeDTO>(); 
            CreateMap<UpdateAssessmentSchemeDTO, AssessmentScheme>();
            CreateMap<CreateAssessmentSchemeDto, AssessmentScheme>().ReverseMap();
            CreateMap<UpdateAssessmentSchemeDto, AssessmentScheme>().ReverseMap();
            CreateMap<AssessmentSchemeRequest,NormalAssessmentSchemeRequest>().ReverseMap();
        }

        private void CreateMapClassTrainerUnit()
        {
            CreateMap<ClassTrainerUnit, InfoTrainer>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Trainer.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Trainer.Email))
             
                .ForMember(x => x.SyllabusId, opt => opt.MapFrom(x => x.TrainingUnit.SyllabusId))
                .ForMember(x=>x.AvatarUrl, opt=>opt.MapFrom(src=>src.Trainer.AvatarUrl));
        }
    }
}
