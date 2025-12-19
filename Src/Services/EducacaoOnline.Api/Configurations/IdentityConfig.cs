using EducacaoOnline.Core.Data;
using Microsoft.AspNetCore.Identity;

namespace EducacaoOnline.Api.Configurations
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UsuariosDbContext>()
                .AddDefaultTokenProviders();

            // policy opcional para usar [Authorize(Policy = "Administrador")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
            });
        }
    }
}
