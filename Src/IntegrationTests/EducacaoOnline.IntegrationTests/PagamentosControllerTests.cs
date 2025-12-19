using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Api.Models.Alunos;
using EducacaoOnline.Api.Models.Cursos;
using EducacaoOnline.Api.Models.Pagamentos;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.IntegrationTests.Factories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace EducacaoOnline.IntegrationTests
{
    public class PagamentosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _httpClient;

        public PagamentosControllerTests()
        {
            var factory = new CustomWebApplicationFactory();
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task AlunoRealizaPagamentoDoCursoComSucesso()
        {
            #region cadastro do curso

            var cursoVm = new CadastrarCursoVm()
            {
                Nome = "Introdução à Filosofia",
                Descricao = "Curso rápido de filosofia",
                Valor = 50,
                MaterialDidatico = "Apostilas disponiveis em: https://www.curso-filosofia.com.br/material",
                NumeroAulas = 2
            };

            var cursoResponse = await _httpClient.PostAsJsonAsync("/api/cursos", cursoVm);
            cursoResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var curso = await cursoResponse.Content.ReadFromJsonAsync<CursoDto>();

            curso.Should().NotBeNull();
            curso.Id.Should().NotBeEmpty();
            #endregion

            #region cadastro do usuário/aluno

            var alunoVm = new CadastrarAlunoRequest()
            {
                Nome = "Albert Einstein",
                Email = "albert.einsten@abc.com",
                ConfirmacaoEmail = "albert.einsten@abc.com",
                Senha = "Albert#2025"
            };

            var alunoResponse = await _httpClient.PostAsJsonAsync($"api/usuarios/", alunoVm);
            var aluno = await alunoResponse.Content.ReadFromJsonAsync<AlunoDto>();

            aluno.Should().NotBeNull();
            aluno.Id.Should().NotBeEmpty();
            aluno.Nome.Should().Be("Albert Einstein");
            aluno.Email.Should().Be("albert.einsten@abc.com");
            #endregion

            #region matricula do aluno no curso

            var matriculaResponse = await _httpClient.PostAsJsonAsync($"api/alunos/{aluno.Id}/matriculas", new NovaMatriculaRequest(aluno.Id, curso.Id));
            var matricula = await matriculaResponse.Content.ReadFromJsonAsync<MatriculaCriadaDto>();

            matricula.Should().NotBeNull();
            matricula.AlunoId.Should().Be(aluno.Id);
            matricula.CursoId.Should().Be(curso.Id);
            matricula.Situacao.Should().Be(SituacaoMatricula.PendenteDePagamento);
            #endregion

            #region realizacao do pagamento

            var validade = DateOnly.FromDateTime(new DateTime(2030, 10, 5));
            var pagamentoVm = new PagamentoRequest(aluno.Id, curso.Id, aluno.Nome, "5215 7777 6633 9678", validade, "292");

            var pagamentoResponse = await _httpClient.PostAsJsonAsync($"api/pagamentos/cursos/{curso.Id}/alunos/{aluno.Id}", pagamentoVm);
            pagamentoResponse.EnsureSuccessStatusCode();
            #endregion
        }

    }
}
