using EducacaoOnline.Conteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Conteudo.Data.Configurations
{
    public class AulaConfiguration : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.ToTable("Aulas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.DataCadastro)
                .IsRequired();

            builder.Property(a => a.CursoId)
                .IsRequired();

            builder.HasOne<Curso>()
                .WithMany(x => x.Aulas)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
