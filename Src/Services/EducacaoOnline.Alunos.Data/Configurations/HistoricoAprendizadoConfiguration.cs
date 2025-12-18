using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Alunos.Data.Configurations
{
    public class HistoricoAprendizadoConfiguration : IEntityTypeConfiguration<HistoricoAprendizado>
    {
        public void Configure(EntityTypeBuilder<HistoricoAprendizado> builder)
        {
            builder.ToTable("HistoricosAprendizado");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(h => h.MatriculaId)
                   .IsRequired();

            builder.Property(h => h.DataConclusao)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.HasOne(h => h.Matricula)
                   .WithOne(m => m.HistoricoAprendizado)
                   .HasForeignKey<HistoricoAprendizado>(h => h.MatriculaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}