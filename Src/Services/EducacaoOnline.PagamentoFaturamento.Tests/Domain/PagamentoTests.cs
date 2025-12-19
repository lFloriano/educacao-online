using System;
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoFaturamento.Domain;
using EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects;
using Xunit;

namespace EducacaoOnline.PagamentoFaturamento.Tests.Domain
{
    public class PagamentoTests
    {
        private DadosCartao CriarCartaoValido()
        {
            var validade = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            return new DadosCartao("Titular Teste", "123456789012", validade, "123");
        }

        [Fact]
        public void Criar_ValorNegativoOuZero_DeveLancarDomainException()
        {
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var cartao = CriarCartaoValido();

            Assert.Throws<DomainException>(() => new Pagamento(alunoId, cursoId, 0m, cartao));
            Assert.Throws<DomainException>(() => new Pagamento(alunoId, cursoId, -10m, cartao));
        }

        [Fact]
        public void Criar_ComDadosValidos_DeveTerStatusPendenteEValoresPreenchidos()
        {
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var cartao = CriarCartaoValido();
            var before = DateTime.Now;

            var pagamento = new Pagamento(alunoId, cursoId, 150m, cartao);

            var after = DateTime.Now;
            Assert.Equal(150m, pagamento.Valor);
            Assert.Equal(alunoId, pagamento.AlunoId);
            Assert.Equal(cursoId, pagamento.CursoId);
            Assert.True(pagamento.Status.EhPendente);
            Assert.InRange(pagamento.DataCadastro, before, after);
            Assert.Same(cartao, pagamento.DadosCartao);
        }

        [Fact]
        public void Confirmar_QuandoPendente_DeveAtribuirStatusConfirmado()
        {
            var pagamento = new Pagamento(Guid.NewGuid(), Guid.NewGuid(), 100m, CriarCartaoValido());

            pagamento.Confirmar();

            Assert.True(pagamento.Status.EhConfirmado);
            Assert.False(pagamento.Status.EhPendente);
        }

        [Fact]
        public void Confirmar_QuandojaProcessado_DeveLancarDomainException()
        {
            var pagamento = new Pagamento(Guid.NewGuid(), Guid.NewGuid(), 100m, CriarCartaoValido());

            pagamento.Confirmar();

            Assert.Throws<DomainException>(() => pagamento.Confirmar());
        }

        [Fact]
        public void Rejeitar_QuandoPendente_DeveAtribuirStatusRejeitadoEMotivo()
        {
            var pagamento = new Pagamento(Guid.NewGuid(), Guid.NewGuid(), 80m, CriarCartaoValido());
            var motivo = "Dados inválidos";

            pagamento.Rejeitar(motivo);

            Assert.True(pagamento.Status.EhRejeitado);
            Assert.Equal(motivo, pagamento.Status.MotivoRejeicao);
        }

        [Fact]
        public void Rejeitar_QuandoJaProcessado_DeveLancarDomainException()
        {
            var pagamento = new Pagamento(Guid.NewGuid(), Guid.NewGuid(), 80m, CriarCartaoValido());

            pagamento.Rejeitar("motivo inicial");

            Assert.Throws<DomainException>(() => pagamento.Rejeitar("outro motivo"));
        }
    }
}