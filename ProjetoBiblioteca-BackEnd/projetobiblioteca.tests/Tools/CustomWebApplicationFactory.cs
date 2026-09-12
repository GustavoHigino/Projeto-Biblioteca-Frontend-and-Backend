using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projetobiblioteca.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace projetobiblioteca.tests.Tools
{
    // Esta classe herda de WebApplicationFactory<TProgram>, o que permite que o xUnit
    // suba a API inteira rodando em memória (TestServer) para realizar testes de integração.
    internal class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        // Guarda a string de conexão dinâmica que aponta para o container do Docker (criado pelo Testcontainers).
        private readonly string _connectionString;
        // O construtor recebe a string de conexão do container Docker que veio da fixture (SqlServerFixture).
        public CustomWebApplicationFactory(
            string connectionString)
        {
            _connectionString = connectionString;
        }
        // Este método permite sobrescrever os comportamentos normais de inicialização da API real.
        protected override void ConfigureWebHost
            (IWebHostBuilder builder)
        {
            // --- ETAPA 1: TROCAR O ARQUIVO DE CONFIGURAÇÃO (APPSETTINGS) ---
            builder.ConfigureAppConfiguration((
                context,
                config) =>
            {
                // Localiza o caminho físico onde o DLL de testes foi compilado
                // e busca o arquivo "appsettings.Test.json" nessa mesma pasta.
                var testConfigPath =
                Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly()
                .Location)!, "appsettings.Test.json");
                // Apaga todas as fontes de configuração originais da API 
                // (ignora o appsettings.json padrão de desenvolvimento/produção).
                config.Sources.Clear();
                // Adiciona obrigatoriamente o "appsettings.Test.json" como fonte principal de configuração.
                config.AddJsonFile(
                    testConfigPath, optional: false,// optional = false garante que se o arquivo não existir, o teste falha na hora.
                    reloadOnChange: true);
                // Redundância de checagem: se o arquivo existir no caminho indicado, recarrega ele.
                if (File.Exists(testConfigPath))
                {
                    config.AddJsonFile(testConfigPath, optional: true, reloadOnChange: true);
                }

                
            });
            // --- ETAPA 2: SUBSTITUIR O BANCO DE DADOS OFICIAL PELO BANCO DO DOCKER ---
            builder.ConfigureServices(
                    services =>
                {
                    // Busca na coleção de Serviços (Injeção de Dependência) a configuração original do DbContext (MSSQL).
                    var descriptor = services
                    .SingleOrDefault
                    (d => d.ServiceType == typeof
                    (DbContextOptions<MSSQL>));
                    // Se encontrou o DbContext original da API, remove ele da memória
                    // para evitar que a API tente se conectar ao banco local ou de desenvolvimento.
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    // Registra novamente o DbContext (MSSQL), mas agora configurando
                    // a string de conexão dinâmica da porta aleatória do container Docker (_connectionString).
                    services.AddDbContext<MSSQL>(
                        options => options
                        .UseSqlServer(
                            _connectionString));
                });




            
        }
    }
}
