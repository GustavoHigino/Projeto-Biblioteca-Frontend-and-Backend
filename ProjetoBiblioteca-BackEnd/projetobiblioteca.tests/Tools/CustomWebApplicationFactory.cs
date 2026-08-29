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
    internal class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        private readonly string _connectionString;
        public CustomWebApplicationFactory(
            string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void ConfigureWebHost
            (IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((
                context,
                config) =>
            {
                var testConfigPath =
                Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly()
                .Location)!, "appsettings.Test.json");
                config.Sources.Clear();
                config.AddJsonFile(
                    testConfigPath, optional: false,
                    reloadOnChange: true);
                if (File.Exists(testConfigPath))
                {
                    config.AddJsonFile(testConfigPath, optional: true, reloadOnChange: true);
                }

                // NOVA MODIFICAÇÃO: Injeta os valores diretamente no IConfiguration da API
                
            });
            builder.ConfigureServices(
                    services =>
                {
                    var descriptor = services
                    .SingleOrDefault
                    (d => d.ServiceType == typeof
                    (DbContextOptions<MSSQL>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddDbContext<MSSQL>(
                        options => options
                        .UseSqlServer(
                            _connectionString));
                });




            
        }
    }
}
