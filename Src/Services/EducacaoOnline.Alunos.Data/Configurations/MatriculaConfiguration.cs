using EducacaoOnline.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Alunos.Data.Configurations
{
    public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.AlunoId)
                .IsRequired();

            builder.Property(m => m.CursoId)
                .IsRequired();

            builder.Property(m => m.CertificadoId)
                .IsRequired(false);

            builder.Property(m => m.Situacao)
                .IsRequired();

            builder.Property(m => m.DataCadastro)
                .IsRequired();

            builder.HasIndex(m => new { m.AlunoId, m.CursoId })
                .IsUnique();

            builder.HasOne(m => m.Certificado)
                .WithOne(c => c.Matricula)
                .HasForeignKey<Certificado>(c => c.MatriculaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.AulasConcluidas)
                .WithOne(c => c.Matricula)
                .HasForeignKey(c => c.MatriculaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}