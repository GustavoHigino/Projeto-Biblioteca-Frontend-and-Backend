using projetobiblioteca.Configuracoes;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;
using Xunit;

namespace projetobiblioteca.tests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; }
        public string ConnectionString =>
            Container.GetConnectionString();
        public SqlServerFixture()
        {
            Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("@Admin123$")
        .Build();
        }
        public async Task InitializeAsync()
        {
            await Container.StartAsync();
            AddEvolve.ExecuteMigration(ConnectionString);
            
        }
        public async Task DisposeAsync()
        {
            await Container.DisposeAsync();
        }
        
    }
}
