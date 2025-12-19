using System;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Core.DomainObjects;
using FluentAssertions;
using Xunit;

namespace EducacaoOnline.Conteudo.Tests.Domain
{
    public class AulaTests
    {
        [Fact]
        public void Criar_ComTituloValido_DevePreencherPropriedades()
        {
            var cursoId = Guid.NewGuid();
            var antes = DateTime.Now;
            var aula = new Aula(cursoId, "Introdução");
            var depois = DateTime.Now;

            aula.CursoId.Should().Be(cursoId);
            aula.Titulo.Should().Be("Introdução");
            aula.DataCadastro.Should().BeOnOrAfter(antes).And.BeOnOrBefore(depois);
        }

        [Fact]
        public void Criar_ComTituloVazio_DeveLancarDomainException()
        {
            Action act = () => new Aula(Guid.NewGuid(), "  ");

            act.Should().Throw<DomainException>()
               .WithMessage("Tilulo da aula não pode ser vazio");
        }
    }
}