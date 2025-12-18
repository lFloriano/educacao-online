using EducacaoOnline.Alunos.Data;
using EducacaoOnline.Api.Configurations.Seeder;
using EducacaoOnline.Conteudo.Data;
using EducacaoOnline.Core.Data;
using EducacaoOnline.PagamentoFaturamento.Data;
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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionLite"))
            );
        }
        else
        {
            builder.Services.AddDbContext<AlunosDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GestaoAlunosConnection"))
            );

            builder.Services.AddDbContext<CursosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlite(builder.Configuration.GetConnectionString("GestaoConteudoConnection"))
            );

            builder.Services.AddDbContext<PagamentosDbContext>(options =>
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                       .UseSqlite(builder.Configuration.GetConnectionString("GestaoFaturamentoConnection"))
            );

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }

    public static void UseMigrationsAndSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var alunosDbContext = scope.ServiceProvider.GetRequiredService<AlunosDbContext>();
            var cursosDbContext = scope.ServiceProvider.GetRequiredService<CursosDbContext>();

            AplicarMigrations(alunosDbContext, cursosDbContext);
            SeederConteudo.Seed(cursosDbContext);
            SeederAlunos.Seed(alunosDbContext);
        }
    }

    private static void AplicarMigrations(AlunosDbContext alunosDbContext, CursosDbContext cursosDbContext)
    {
        alunosDbContext.Database.Migrate();
        cursosDbContext.Database.Migrate();
        //TODO: add context de faturamento
    }
}
