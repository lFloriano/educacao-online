using EducacaoOnline.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Alunos.Data.Configurations
{
    public class AulaConcluidaConfiguration : IEntityTypeConfiguration<AulaConcluida>
    {
        public void Configure(EntityTypeBuilder<AulaConcluida> builder)
        {
            builder.ToTable("AulasConcluidas");
            builder.HasKey(a => new { a.MatriculaId, a.AulaId });

            builder.Property(a => a.AulaId)
                .IsRequired();

            builder.Property(a => a.MatriculaId)
                .IsRequired();

            builder.HasIndex(a => new { a.AulaId, a.MatriculaId })
                .IsUnique();
        }
    }
}