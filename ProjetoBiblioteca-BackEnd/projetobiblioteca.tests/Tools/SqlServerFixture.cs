using projetobiblioteca.Configuracoes;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;
using Xunit;

namespace projetobiblioteca.tests.Tools
{
    // A interface IAsyncLifetime indica ao xUnit que esta classe possui inicialização e descarte assíncronos.
    public class SqlServerFixture : IAsyncLifetime
    {
        // Propriedade para gerenciar o container Docker do SQL Server.
        public MsSqlContainer Container { get; }
        // Recupera a string de conexão dinâmica gerada pelo container rodando em uma porta aleatória.
        //A propriedade ConnectionString busca a string pronta diretamente de dentro do Container
        public string ConnectionString =>
            Container.GetConnectionString();

        // Construtor: Configura a imagem do SQL Server que será baixada e executada.
        public SqlServerFixture()
        {
            // Define a imagem oficial do SQL Server 2022 e define a senha do SA.
            Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("@Admin123$")
        .Build();
        }
        // Executado uma única vez pelo xUnit ANTES de rodar os testes da classe.
        public async Task InitializeAsync()
        {
            // Inicia o container Docker do SQL Server.
            await Container.StartAsync();
            // Executa as migrações (Evolve) criando a estrutura de tabelas no banco de dados zerado do container.
            AddEvolve.ExecuteMigration(ConnectionString);

        }
        // Executado pelo xUnit APÓS a finalização dos testes para limpar recursos.
        public async Task DisposeAsync()
        {
            // Encerra e remove o container Docker, garantindo ambiente limpo para as próximas execuções.
            await Container.DisposeAsync();
        }
        
    }
}
