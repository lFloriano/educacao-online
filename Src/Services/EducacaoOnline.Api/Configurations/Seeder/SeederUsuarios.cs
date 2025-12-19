using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Enums;
using EducacaoOnline.Core.Extensions;
using Microsoft.AspNetCore.Identity;

namespace EducacaoOnline.Api.Configurations.Seeder
{
    public static class SeederUsuarios
    {
        public static void Seed(UsuariosDbContext usuariosDbContext, UserManager<IdentityUser> userManager)
        {
            GerarRoles(usuariosDbContext);
            GerarUsuarios(usuariosDbContext, userManager);
        }

        private static void GerarRoles(UsuariosDbContext usuariosDbContext)
        {
            if (usuariosDbContext.Roles.Any())
                return;

            var tiposUsuario = Enum.GetValues(typeof(TipoUsuario)).Cast<TipoUsuario>();

            foreach (var tipo in tiposUsuario)
            {
                var nomeRole = tipo.GetDescription();
                var nomeRoleNormalizado = nomeRole.ToUpperInvariant();

                if (!usuariosDbContext.Roles.Any(r => r.NormalizedName == nomeRoleNormalizado))
                {
                    var novaRole = new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = nomeRole,
                        NormalizedName = nomeRoleNormalizado,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    usuariosDbContext.Roles.Add(novaRole);
                }
            }

            usuariosDbContext.SaveChanges();
        }

        private static void GerarUsuarios(UsuariosDbContext usuariosDbContext, UserManager<IdentityUser> userManager)
        {
            var emailAdmin = "admin@educacao.com";
            var adminExistente = userManager.FindByEmailAsync(emailAdmin).Result;

            if (adminExistente != null)
                return;

            var novoAdmin = new IdentityUser()
            {
                UserName = "Administrador",
                Email = emailAdmin,
                NormalizedUserName = "Administrador",
                NormalizedEmail = emailAdmin,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var resultadoCriacao = userManager.CreateAsync(novoAdmin, "Admin@123").Result;

            if (resultadoCriacao.Succeeded)
            {
                userManager
                    .AddToRoleAsync(novoAdmin, TipoUsuario.Administrador.GetDescription().ToUpper())
                    .GetAwaiter()
                    .GetResult();
            }

            usuariosDbContext.SaveChanges();
        }
    }
}
