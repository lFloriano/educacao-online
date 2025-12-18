using EducacaoOnline.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Alunos.Data.Configurations
{
    public class CertificadoConfiguration : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificados");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.MatriculaId)
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .IsRequired();

            builder.HasIndex(c => c.MatriculaId)
                .IsUnique();

            builder.HasOne(c => c.Matricula)
                   .WithOne(m => m.Certificado)
                   .HasForeignKey<Certificado>(c => c.MatriculaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}