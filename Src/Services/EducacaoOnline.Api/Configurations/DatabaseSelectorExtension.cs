using EducacaoOnline.Alunos.Data;
using EducacaoOnline.Api.Configurations.Seeder;
using EducacaoOnline.Conteudo.Data;
using EducacaoOnline.Core.Data;
using EducacaoOnline.PagamentoFaturamento.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Api.Configurations;

public static partial class DatabaseSelectorExtension
{
    public static void AddDatabaseSelector(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<AlunosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlite(builder.Configuration.GetConnectionString("GestaoAlunosConnectionLite"))
            );

            builder.Services.AddDbContext<CursosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlite(builder.Configuration.GetConnectionString("GestaoConteudoConnectionLite"))
            );

            builder.Services.AddDbContext<PagamentosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlite(builder.Configuration.GetConnectionString("GestaoFaturamentoConnectionLite"))
            );

            builder.Services.AddDbContext<UsuariosDbContext>(options =>
                   options.UseSqlite(builder.Configuration.GetConnectionString("GestaoUsuariosConnectionLite"))
            );
        }
        else if (builder.Environment.IsEnvironment("Testing"))
        {

        }
        else
        {
            builder.Services.AddDbContext<AlunosDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GestaoAlunosConnection"))
            );

            builder.Services.AddDbContext<CursosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlServer(builder.Configuration.GetConnectionString("GestaoConteudoConnection"))
            );

            builder.Services.AddDbContext<PagamentosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlServer(builder.Configuration.GetConnectionString("GestaoFaturamentoConnection"))
            );

            builder.Services.AddDbContext<UsuariosDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("GestaoUsuariosConnectionLite"))
            );
        }
    }

    public static void UseMigrationsAndSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var alunosDbContext = scope.ServiceProvider.GetRequiredService<AlunosDbContext>();
            var cursosDbContext = scope.ServiceProvider.GetRequiredService<CursosDbContext>();
            var pagamentosDbContext = scope.ServiceProvider.GetRequiredService<PagamentosDbContext>();
            var usuariosDbContext = scope.ServiceProvider.GetRequiredService<UsuariosDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            AplicarMigrations(alunosDbContext, cursosDbContext, pagamentosDbContext, usuariosDbContext);
            SeederUsuarios.Seed(usuariosDbContext, userManager);
            SeederConteudo.Seed(cursosDbContext);
            SeederAlunos.Seed(alunosDbContext);
        }
    }

    public static void UseSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var alunosDbContext = scope.ServiceProvider.GetRequiredService<AlunosDbContext>();
            var cursosDbContext = scope.ServiceProvider.GetRequiredService<CursosDbContext>();
            var pagamentosDbContext = scope.ServiceProvider.GetRequiredService<PagamentosDbContext>();
            var usuariosDbContext = scope.ServiceProvider.GetRequiredService<UsuariosDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            SeederUsuarios.Seed(usuariosDbContext, userManager);
            SeederConteudo.Seed(cursosDbContext);
            SeederAlunos.Seed(alunosDbContext);
        }
    }

    private static void AplicarMigrations(
        AlunosDbContext alunosDbContext,
        CursosDbContext cursosDbContext,
        PagamentosDbContext pagamentosDbContext,
        UsuariosDbContext usuariosDbContext)
    {
        alunosDbContext.Database.Migrate();
        cursosDbContext.Database.Migrate();
        pagamentosDbContext.Database.Migrate();
        usuariosDbContext.Database.Migrate();
    }
}
