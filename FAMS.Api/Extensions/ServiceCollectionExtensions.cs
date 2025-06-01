using FAMS.Api.Repositories;
using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Core.Repositories;
using FAMS.Core.Repositories.Interfaces;

namespace FAMS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            
            // Configure AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register repositories here
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepo, UserRepository>();
            services.AddScoped<ISyllabusRepo, SyllabusRepository>();

            // Register services here
            /*services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped<IUserPermissionService, UserPermissionServices>();
            services.AddScoped<IAuthService, AuthService>();*/

            services.AddScoped<ISyllabusService, SyllabusService>();
            services.AddScoped<ITrainingContentService, TrainingContentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
   
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<ITrainingUnitService, TrainingUnitService>();
            services.AddScoped<ITrainingProgramService, TrainingProgramService>();
            services.AddScoped<ILearningObjectiveService, LearningObjectiveService>();

            services.AddScoped<ITrainingProgramService, TrainingProgramService>();
            services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();

            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IMaterialService, MaterialService>();

            services.AddScoped<ICalendarService,CalendarService>();
            services.AddScoped<IAssessmentSchemeService,AssessmentSchemeService>();
            return services;
        }
    }
}
