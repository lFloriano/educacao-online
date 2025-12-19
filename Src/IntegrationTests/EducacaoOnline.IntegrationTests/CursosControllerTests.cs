using EducacaoOnline.IntegrationTests.Factories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Get_ObterTodos_DeveRetornarSucesso()
        {
            //arrange

            //act
            var response = await _httpClient.GetAsync("/api/cursos");

            //assert
            response.EnsureSuccessStatusCode();
        }

    }
}
