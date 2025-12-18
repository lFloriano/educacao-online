using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.ValueObjects;
using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Alunos.Data
{
    public class AlunosDbContext : DbContext, IUnitOfWork
    {

        public AlunosDbContext(DbContextOptions<AlunosDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<HistoricoAprendizado> HistoricosAprendizado { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlunosDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
