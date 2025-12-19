using EducacaoOnline.Api.Models.Cursos;
using EducacaoOnline.IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
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

        [Fact()]
        public async Task ObterTodosOsCursosDeveRetornarSucesso()
        {
            //arrange

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
    }
}
