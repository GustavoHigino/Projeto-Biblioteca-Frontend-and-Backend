using projetobiblioteca.Data;
using projetobiblioteca.FileExport.Exporter.Contract;
using projetobiblioteca.FileExport.Exporter.Impl;
using projetobiblioteca.Model;

namespace projetobiblioteca.FileExport.Exporter.Factory
{
    public class FileExporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileExporterFactory>
            _logger;
        public FileExporterFactory(IServiceProvider serviceProvider,
            ILogger<FileExporterFactory> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public IFileExporter<Aluno> GetExporter(
            string acceptHeader)
        {
            if(string.Equals(acceptHeader,
                MediaTypes.ApplicationXlsx,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation(
                    $"Selected excel file exporter" +
                    $" for media type {acceptHeader}");
                return _serviceProvider.GetService
                    <XlsxExporterAluno<Aluno>>();

            }
            else if(string.Equals(acceptHeader,
                MediaTypes.ApplicationCsv,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation(
                    $"Selected csv file exporter for " +
                    $"media type {acceptHeader}");
                return _serviceProvider.GetService<CsvExporterAluno<Aluno>>();
            }
            else
            {
                _logger.LogError($"Unsupported " +
                    $"media type {acceptHeader}");
                throw new NotSupportedException(
                    $"The media Type of " +
                    $"{acceptHeader} is not supported");
            }
        }
    }
}
