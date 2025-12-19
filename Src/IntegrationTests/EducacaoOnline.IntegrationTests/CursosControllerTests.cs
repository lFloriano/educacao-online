using EducacaoOnline.Api.Models.Cursos;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.IntegrationTests.Factories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace EducacaoOnline.IntegrationTests
{
    public class CursosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _httpClient;

        public CursosControllerTests()
        {
            var factory = new CustomWebApplicationFactory();
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task ObterTodosOsCursosDeveRetornarSucesso()
        {
            //act
            var response = await _httpClient.GetAsync("/api/cursos");

            //assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AdminCadastraCursoComSucesso()
        {
            //arrange
            var curso = new CadastrarCursoVm()
            {
                Nome = "Introdução à Filosofia",
                Descricao = "Curso rápido de filosofia",
                Valor = 50,
                MaterialDidatico = "Apostilas disponiveis em: https://www.curso-filosofia.com.br/material",
                NumeroAulas = 2
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/api/cursos", curso);

            //assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AdminCadastraAulaComSucesso()
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

            #region cadastro da aula

            var aulaVm = new CadastrarAulaVm()
            {
                CursoId = curso.Id,
                Titulo = "Escritos Aristotélicos"
            };

            var aulaResponse = await _httpClient.PostAsJsonAsync($"api/cursos/{curso.Id.ToString()}/aulas", aulaVm);
            var aula = await aulaResponse.Content.ReadFromJsonAsync<AulaDto>();

            aula.Should().NotBeNull();
            aula.Id.Should().NotBeEmpty();
            aula.Titulo.Should().Be("Escritos Aristotélicos");
            #endregion
        }

        [Fact]
        public async Task ConsultaDeCursoPorIdBemSucedida()
        {
            #region cadastro de curso

            var cursoVm = new CadastrarCursoVm()
            {
                Nome = "Introdução à Filosofia",
                Descricao = "Curso rápido de filosofia",
                Valor = 50,
                MaterialDidatico = "Apostilas disponiveis em: https://www.curso-filosofia.com.br/material",
                NumeroAulas = 2
            };

            var cursoResponse = await _httpClient.PostAsJsonAsync("/api/cursos", cursoVm);
            var curso = await cursoResponse.Content.ReadFromJsonAsync<CursoDto>();
            cursoResponse.EnsureSuccessStatusCode();

            #endregion

            #region consulta curso criado

            var consultaResponse = await _httpClient.GetAsync($"/api/cursos/{curso.Id}");
            var cursoConsultado = await consultaResponse.Content.ReadFromJsonAsync<CursoDto>();


            cursoConsultado.Should().NotBeNull();
            cursoConsultado.Id.Should().Be(curso.Id);
            cursoConsultado.Nome.Should().Be(curso.Nome);
            #endregion

        }

        [Fact]
        public async Task ConsultaDeAulasPorCursoIdBemSucedida()
        {
            #region cadastro de curso

            var cursoVm = new CadastrarCursoVm()
            {
                Nome = "Introdução à Filosofia",
                Descricao = "Curso rápido de filosofia",
                Valor = 50,
                MaterialDidatico = "Apostilas disponiveis em: https://www.curso-filosofia.com.br/material",
                NumeroAulas = 2
            };

            var cursoResponse = await _httpClient.PostAsJsonAsync("/api/cursos", cursoVm);
            var curso = await cursoResponse.Content.ReadFromJsonAsync<CursoDto>();
            cursoResponse.EnsureSuccessStatusCode();

            #endregion

            #region consulta curso criado

            var consultaResponse = await _httpClient.GetAsync($"/api/cursos/{curso.Id}");
            var cursoConsultado = await consultaResponse.Content.ReadFromJsonAsync<CursoDto>();


            cursoConsultado.Should().NotBeNull();
            cursoConsultado.Id.Should().Be(curso.Id);
            cursoConsultado.Nome.Should().Be(curso.Nome);
            #endregion

            #region cadastro da aula
            var aulaVm = new CadastrarAulaVm()
            {
                CursoId = curso.Id,
                Titulo = "Escritos Aristotélicos"
            };

            var aulaResponse = await _httpClient.PostAsJsonAsync($"api/cursos/{curso.Id.ToString()}/aulas", aulaVm);
            var aula = await aulaResponse.Content.ReadFromJsonAsync<AulaDto>();

            aula.Should().NotBeNull();
            aula.Id.Should().NotBeEmpty();
            aula.Titulo.Should().Be("Escritos Aristotélicos");
            #endregion

            #region consulta curso criado

            var consultaAulasResponse = await _httpClient.GetAsync($"/api/cursos/{curso.Id}/aulas");
            var aulasConsultadas = await consultaAulasResponse.Content.ReadFromJsonAsync<IEnumerable<AulaDto>>();

            aulasConsultadas.Should().NotBeNull();
            consultaAulasResponse.EnsureSuccessStatusCode();
            #endregion
        }
    }
}
