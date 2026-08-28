using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Model;
using projetobiblioteca.tests.Tools;
using System.Net.Http.Json;

namespace projetobiblioteca.tests.Cors
{
    public class CorsTest
    {
        private readonly HttpClient _http;
        private static TokenDto _token;
        private static Aluno _student;
        public CorsTest(SqlServerFixture sqlFixture)
        {
            var factory = new
                CustomWebApplicationFactory<Program>(
                sqlFixture.ConnectionString);
            _http = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri(
                        "http://localhost")
                });
        }
        [Fact]
        public async Task Signin_ShouldReturnToken()

        {
            var credentials = new UserDto
                ("gustavo", "123456");
            
            var response = await _http.PostAsJsonAsync
                ("auth/signin", credentials);
            response.EnsureSuccessStatusCode();
            var token = await response.Content
                .ReadFromJsonAsync<TokenDto>();
            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeNullOrWhiteSpace();
            token.RefreshToken.Should().NotBeNullOrWhiteSpace();
            _token = token;
               
        }
    }
}
