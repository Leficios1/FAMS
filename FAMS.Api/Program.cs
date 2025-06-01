using FAMS.Api.Configurations.Authorization;
using FAMS.Api.Configurations.Cors;
using FAMS.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FAMS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            Extensions.ServiceCollectionExtensions.Register(builder.Services);
            Configurations.Databases.SqlServerConfiguration.ConfigureSqlServer(builder.Services, builder.Configuration);
            Configurations.Jwt.JwtConfiguration.ConfigureJwt(builder.Services, builder.Configuration);
            Configurations.Swagger.SwaggerConfiguration.ConfigureSwagger(builder.Services);
            //builder.Services.AddScoped<IAuthorizationMiddlewareResultHandler, DynamicAuthorization>();
            


            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.ConfigureCors(builder.Configuration);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
         /*   app.UseMiddleware<DynamicAuthorization>();*/
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>  
            {  
              endpoints.MapControllers();  
            });

            app.Run();
        }
    }
}
