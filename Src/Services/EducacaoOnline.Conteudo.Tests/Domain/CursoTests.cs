using System;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Conteudo.Domain.ValueObjects;
using EducacaoOnline.Core.DomainObjects;
using FluentAssertions;
using Xunit;

namespace EducacaoOnline.Conteudo.Tests.Domain
{
    public class CursoTests
    {
        [Fact]
        public void Criar_ComValoresValidos_DeveCriar()
        {
            var conteudo = new ConteudoProgramatico(2, "Material");
            var curso = new Curso("Nome do Curso", "Descricao", 0m, conteudo);

            curso.Nome.Should().Be("Nome do Curso");
            curso.Descricao.Should().Be("Descricao");
            curso.Valor.Should().Be(0m);
            curso.Aulas.Should().BeEmpty();
            curso.ConteudoProgramatico.Should().BeSameAs(conteudo);
        }

        [Fact]
        public void Criar_ComNomeVazio_DeveLancarDomainException()
        {
            Action act = () => new Curso("  ", "Descricao", 10m, new ConteudoProgramatico(1, "m"));
            act.Should().Throw<DomainException>()
               .WithMessage("O nome do curso não pode ser vazio");
        }

        [Fact]
        public void CadastrarAula_DeveAdicionarAula()
        {
            var conteudo = new ConteudoProgramatico(5, "m");
            var curso = new Curso("N", "D", 10m, conteudo);

            var aula = new Aula(Guid.NewGuid(), "Aula 1");
            var returned = curso.CadastrarAula(aula);

            curso.Aulas.Should().ContainSingle().Which.Should().Be(aula);
            returned.Should().Be(aula);
        }

        [Fact]
        public void CadastrarAula_AoAdicionarMesmaInstancia_DeveLancarDomainException()
        {
            var conteudo = new ConteudoProgramatico(5, "m");
            var curso = new Curso("N", "D", 10m, conteudo);

            var aula = new Aula(Guid.NewGuid(), "Aula 1");
            curso.CadastrarAula(aula);

            Action act = () => curso.CadastrarAula(aula);
            act.Should().Throw<DomainException>()
               .WithMessage("Aula já existe no curso");
        }

        [Fact]
        public void CadastrarAula_ComMesmoTituloCaseInsensitive_DeveLancarDomainException()
        {
            var conteudo = new ConteudoProgramatico(5, "m");
            var curso = new Curso("N", "D", 10m, conteudo);

            var aula1 = new Aula(Guid.NewGuid(), "Aula X");
            var aula2 = new Aula(Guid.NewGuid(), "aula x");

            curso.CadastrarAula(aula1);
            Action act = () => curso.CadastrarAula(aula2);

            act.Should().Throw<DomainException>()
               .WithMessage("Já existe aula com mesmo título neste curso");
        }

        [Fact]
        public void CadastrarAula_QuandoNumeroMaximoAtingido_DeveLancarDomainException()
        {
            var conteudo = new ConteudoProgramatico(1, "m");
            var curso = new Curso("N", "D", 10m, conteudo);

            var aula1 = new Aula(Guid.NewGuid(), "A1");
            var aula2 = new Aula(Guid.NewGuid(), "A2");

            curso.CadastrarAula(aula1);

            Action act = () => curso.CadastrarAula(aula2);
            act.Should().Throw<DomainException>()
               .WithMessage("Número máximo de aulas atingido");
        }
    }
}