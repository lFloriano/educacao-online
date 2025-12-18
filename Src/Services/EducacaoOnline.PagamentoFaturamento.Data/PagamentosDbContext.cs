using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Messages;
using EducacaoOnline.PagamentoFaturamento.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.PagamentoFaturamento.Data
{
    public class PagamentosDbContext : DbContext, IUnitOfWork
    {
        public PagamentosDbContext(DbContextOptions<PagamentosDbContext> options) : base(options) { }

        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosDbContext).Assembly);
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
