using Microsoft.OpenApi.Models;

namespace FAMS.Api.Configurations.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "FAMS", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                    },
                        Array.Empty<string>()
                    }
                });
                // Bug appears at this line
                // { Code = 500, Message = Cannot modify ServiceCollection after application is built. }
                //services.AddAuthorization(options =>
                //{
                //    options.AddPolicy("Admin", policy =>
                //    {
                //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                //        policy.RequireAuthenticatedUser();
                //    });
                //});
            });

            return services;
        }
    }
}
