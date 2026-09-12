using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using projetobiblioteca.Data.DTO.Aluno;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Model;
using projetobiblioteca.tests.Tools;
using System.Net.Http.Json;
using Xunit;
namespace projetobiblioteca.tests.Cors
{
    // Informa ao xUnit para usar o nosso "PriorityOrderer" para organizar a ordem de execução dos testes.
    // O primeiro parâmetro é o caminho completo da classe e o segundo é o nome do projeto (Assembly).
    [TestCaseOrderer
        //caminho que leva ate o priority orderer class
        ("projetobiblioteca.tests.Tools.PriorityOrderer",
        //caminho do projeto
        "projetobiblioteca.tests")]
    // Indica ao xUnit que esta classe de testes precisa do "SqlServerFixture" (que sobe o container Docker do banco).
    // O xUnit vai instanciar o SqlServerFixture UMA ÚNICA VEZ antes de rodar os testes desta classe.
    public class AlunoTest : IClassFixture<SqlServerFixture>
    {
        // Variável estática que guarda o cliente HTTP.
        // Por ser 'static', o mesmo cliente HTTP (e seus cookies de autenticação) é mantido vivo 
        // entre a execução dos diferentes métodos de teste.
        private static HttpClient _http;
        // O xUnit injeta a mesma instância do 'SqlServerFixture' criada no início através do construtor.
        public AlunoTest(SqlServerFixture sqlFixture)
        {
            // Padrão Singleton: Só cria a API em memória e o cliente HTTP na primeira vez que a classe for instanciada.
            // Nas próximas invocações (quando o xUnit for rodar o teste 01, 02, etc.), o '_http' já não será nulo e será reaproveitado.
            if (_http == null)
            {
                // Instancia a fábrica customizada da API, passando a string de conexão dinâmica do container Docker.

                var factory = new
                    CustomWebApplicationFactory<Program>(
                    sqlFixture.ConnectionString);
                _http = factory.CreateClient(
                    new WebApplicationFactoryClientOptions
                    {
                        BaseAddress = new Uri(
                            "https://localhost"),
                        AllowAutoRedirect = false,
                        HandleCookies = true,// IMPORTANTE: Armazena e envia automaticamente os cookies entre as requisições
                    });
            }
        }
        [Fact(DisplayName ="00-Sign in")]
        [TestPriority(0)]
        public async Task Signin_ShouldReturnToken()

        {
            var credentials = new UserDto
                ("gustavo", "123456");
            
            var response = await _http.PostAsJsonAsync
                ("Auth/signin", credentials);
            response.EnsureSuccessStatusCode();
            var token = await response.Content
                .ReadAsStringAsync();
            token.Should().NotBeNull();
            token.Should().Be("login done with successfully");
               
        }
        [Fact(DisplayName ="01-PostALuno")]
        [TestPriority(1)]
        public async Task CreatingAnAluno()
        {
            var content = new AlunoDto("Rus", "Contabeis",
                "M", "Rua A Quadra B"
                , "98989-8888", "balblablal@gmail.com", new DateTime(2000, 12, 9));
            //{
            //    Genero = "M",
            //    Curso = "Contabeis",
            //    Email = "balblablal@gmail.com",
            //    Endereço = "rua A quadra B",
            //    Nascimento = new DateTime(2000, 12, 9),
            //    Nome = "Rus",
            //    Telefone = "99598-9102"


            //};
            var response= await _http.PostAsJsonAsync("Alunos", content);
            response.EnsureSuccessStatusCode();
            var aluno= await response.Content.ReadFromJsonAsync<AlunoDto>();
            aluno.Should().NotBeNull();
            aluno.Nome.Should().Be(content.Nome);
            aluno.Email.Should().Be(content.Email);
        }
    }
}
