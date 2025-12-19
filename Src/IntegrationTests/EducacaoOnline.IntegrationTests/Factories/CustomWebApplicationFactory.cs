using EducacaoOnline.Alunos.Data;
using EducacaoOnline.Conteudo.Data;
using EducacaoOnline.Core.Data;
using EducacaoOnline.PagamentoFaturamento.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EducacaoOnline.IntegrationTests.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection? _connUsuarios;
        private SqliteConnection? _connAlunos;
        private SqliteConnection? _connCursos;
        private SqliteConnection? _connPagamentos;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                RemoverDbContexts(services);
                CriarSqliteConnections();
                AddDbContexts(services);
                GarantirCriacaoDosBancos(services);
                AddAutenticacao(services);
            });
        }

        private void RemoverDbContexts(IServiceCollection services)
        {
            services.RemoveAll(typeof(DbContextOptions<AlunosDbContext>));
            services.RemoveAll(typeof(DbContextOptions<CursosDbContext>));
            services.RemoveAll(typeof(DbContextOptions<PagamentosDbContext>));
            services.RemoveAll(typeof(DbContextOptions<UsuariosDbContext>));
        }

        private void CriarSqliteConnections()
        {
            _connUsuarios = new SqliteConnection("DataSource=file:testsUsuarios?mode=memory&cache=shared");
            _connUsuarios.Open();

            _connAlunos = new SqliteConnection("DataSource=file:testsAlunos?mode=memory&cache=shared");
            _connAlunos.Open();

            _connCursos = new SqliteConnection("DataSource=file:testsCursos?mode=memory&cache=shared");
            _connCursos.Open();

            _connPagamentos = new SqliteConnection("DataSource=file:testsPagamentos?mode=memory&cache=shared");
            _connPagamentos.Open();
        }

        private void AddDbContexts(IServiceCollection services)
        {
            services.AddDbContext<UsuariosDbContext>(opts => opts.UseSqlite(_connUsuarios));
            services.AddDbContext<AlunosDbContext>(opts => opts.UseSqlite(_connAlunos));
            services.AddDbContext<CursosDbContext>(opts => opts.UseSqlite(_connCursos));
            services.AddDbContext<PagamentosDbContext>(opts => opts.UseSqlite(_connPagamentos));
        }

        private void GarantirCriacaoDosBancos(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            scope.ServiceProvider.GetRequiredService<AlunosDbContext>().Database.EnsureCreated();
            scope.ServiceProvider.GetRequiredService<CursosDbContext>().Database.EnsureCreated();
            scope.ServiceProvider.GetRequiredService<PagamentosDbContext>().Database.EnsureCreated();
            scope.ServiceProvider.GetRequiredService<UsuariosDbContext>().Database.EnsureCreated();
        }

        private void AddAutenticacao(IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connUsuarios?.Dispose();
                _connAlunos?.Dispose();
                _connCursos?.Dispose();
                _connPagamentos?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}