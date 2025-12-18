using Microsoft.OpenApi.Models;

namespace EducacaoOnline.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EducacaoOnline API",
                    Version = "v1"
                });

                var bearerScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autorização JWT. Cole o token no campo a seguir",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", bearerScheme);
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        { bearerScheme, Array.Empty<string>() }
                    });

                options.EnableAnnotations();
            });
        }

        public static void UsarSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EducacaoOnline API v1");
            });
        }
    }
}