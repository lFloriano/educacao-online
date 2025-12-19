using System;
using EducacaoOnline.Conteudo.Domain.ValueObjects;
using EducacaoOnline.Core.DomainObjects;
using FluentAssertions;
using Xunit;

namespace EducacaoOnline.Conteudo.Tests.Domain
{
    public class ConteudoProgramaticoTests
    {
        [Fact]
        public void Criar_ComValoresValidos_DeveCriar()
        {
            var cp = new ConteudoProgramatico(10, "  Material de Apoio  ");

            cp.NumeroAulas.Should().Be(10);
            cp.MaterialDidatico.Should().Be("Material de Apoio");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void Criar_ComNumeroAulasInvalido_DeveLancarDomainException(int numero)
        {
            Action act = () => new ConteudoProgramatico(numero, "Material");

            act.Should().Throw<DomainException>()
               .WithMessage("O número de aulas deve estar entre 0 e 100");
        }

        [Fact]
        public void Criar_ComMaterialVazio_DeveLancarDomainException()
        {
            Action act = () => new ConteudoProgramatico(5, "   ");

            act.Should().Throw<DomainException>()
               .WithMessage("O material didático não pode ser vazio.");
        }

        [Fact]
        public void Equals_E_GetHashCode_DeveConsiderarMaterialCaseInsensitive()
        {
            var a = new ConteudoProgramatico(3, "Manuais");
            var b = new ConteudoProgramatico(3, "manuais");

            a.Equals(b).Should().BeTrue();
            a.GetHashCode().Should().Be(b.GetHashCode());
            (a == b).Should().BeTrue();
        }
    }
}