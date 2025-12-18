using EducacaoOnline.Conteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Conteudo.Data.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.Valor)
                .IsRequired();

            //Value Object
            builder.OwnsOne(c => c.ConteudoProgramatico, conteudo =>
            {
                conteudo.Property(cp => cp.NumeroAulas)
                    .HasColumnName("NumeroAulas")
                    .IsRequired();

                conteudo.Property(cp => cp.MaterialDidatico)
                    .HasColumnName("MaterialDidatico")
                    .HasMaxLength(500)
                    .IsRequired();
            });

            builder.HasMany(x => x.Aulas)
                .WithOne()
                .HasForeignKey(x => x.CursoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}