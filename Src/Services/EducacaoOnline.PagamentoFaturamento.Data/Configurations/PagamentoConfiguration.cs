using EducacaoOnline.PagamentoFaturamento.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducacaoOnline.PagamentoFaturamento.Data.Configurations
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AlunoId)
                .IsRequired();

            builder.Property(p => p.CursoId)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired();

            builder.Property(p => p.Valor)
                .IsRequired();

            builder.HasIndex(p => new { p.AlunoId, p.CursoId })
                .IsUnique();
                        
            builder.OwnsOne(p => p.DadosCartao, dc =>
            {
                dc.Property(c => c.Titular)
                    .HasColumnName("CartaoTitular")
                    .HasMaxLength(150)
                    .IsRequired();

                dc.Property(c => c.Numero)
                    .HasColumnName("CartaoNumero")
                    .HasMaxLength(20)
                    .IsRequired();

                dc.Property(c => c.Validade)
                    .HasColumnName("CartaoValidade")
                    .IsRequired();

                dc.Property(c => c.CVV)
                    .HasColumnName("CartaoCVV")
                    .HasMaxLength(5)
                    .IsRequired();
            });
                        
            builder.OwnsOne(p => p.Status, sp =>
            {
                sp.Property(s => s.Status)
                    .HasColumnName("StatusPagamento")
                    .HasMaxLength(20)
                    .IsRequired();

                sp.Property(s => s.MotivoRejeicao)
                    .HasColumnName("MotivoRejeicao")
                    .HasMaxLength(255);
            });
        }
    }

}
