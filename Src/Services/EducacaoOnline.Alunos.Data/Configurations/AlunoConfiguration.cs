using EducacaoOnline.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.Alunos.Data.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(a => a.DataCadastro)
                   .IsRequired();

            builder.HasIndex(a => a.Email)
                   .IsUnique();

            builder.HasMany(x => x.Matriculas)
                .WithOne(x => x.Aluno)
                .HasForeignKey(x => x.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}