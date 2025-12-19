using EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects;
using Xunit;

namespace EducacaoOnline.PagamentoFaturamento.Tests.Domain
{
    public class StatusPagamentoTests
    {
        [Fact]
        public void Criar_Pendente_DeveRetornarStatusPendente()
        {
            var status = StatusPagamento.Pendente();

            Assert.True(status.EhPendente);
            Assert.False(status.EhConfirmado);
            Assert.False(status.EhRejeitado);
            Assert.Null(status.MotivoRejeicao);
        }

        [Fact]
        public void Criar_Confirmado_DeveRetornarStatusConfirmado()
        {
            var status = StatusPagamento.Confirmado();

            Assert.True(status.EhConfirmado);
            Assert.False(status.EhPendente);
            Assert.False(status.EhRejeitado);
        }

        [Fact]
        public void Criar_Rejeitado_DeveRetornarRejeitavoEMotivo()
        {
            var motivo = "Cartão recusado";
            var status = StatusPagamento.Rejeitado(motivo);

            Assert.True(status.EhRejeitado);
            Assert.False(status.EhPendente);
            Assert.False(status.EhConfirmado);
            Assert.Equal(motivo, status.MotivoRejeicao);
        }
    }
}