using System;
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoFaturamento.Domain.ValueObjects;
using Xunit;

namespace EducacaoOnline.PagamentoFaturamento.Tests.Domain
{
    public class DadosCartaoTests
    {
        [Fact]
        public void Criar_ComTitularInvalido_DeveLancarDomainException()
        {
            var validade = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            Assert.Throws<DomainException>(() => new DadosCartao("", "123456789012", validade, "123"));
            Assert.Throws<DomainException>(() => new DadosCartao("   ", "123456789012", validade, "123"));
        }

        [Fact]
        public void Criar_ComNumeroInvalido_DeveLancarDomainException()
        {
            var validade = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            Assert.Throws<DomainException>(() => new DadosCartao("Titular", "", validade, "123"));
            Assert.Throws<DomainException>(() => new DadosCartao("Titular", "1234567", validade, "123")); // < 12
        }

        [Fact]
        public void Criar_ComDadosValidos_DeveCriar()
        {
            var validade = DateOnly.FromDateTime(DateTime.Now.AddYears(2));
            var titular = "Nome Titular";
            var numero = "123456789012";
            var cvv = "999";

            var cartao = new DadosCartao(titular, numero, validade, cvv);

            Assert.Equal(titular, cartao.Titular);
            Assert.Equal(numero, cartao.Numero);
            Assert.Equal(validade, cartao.Validade);
            Assert.Equal(cvv, cartao.CVV);
        }
    }
}