using projetobiblioteca.Data;
using projetobiblioteca.FileExport.Exporter.Contract;
using projetobiblioteca.FileExport.Exporter.Impl;
using projetobiblioteca.Model;

namespace projetobiblioteca.FileExport.Exporter.Factory
{
    public class FileExporterFactory<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileExporterFactory<T>>
            _logger;
        public FileExporterFactory(IServiceProvider serviceProvider,
            ILogger<FileExporterFactory<T>> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public IFileExporter<T> GetExporter(
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
                    <XlsxExporterAluno<T>>();

            }
            else if(string.Equals(acceptHeader,
                MediaTypes.ApplicationCsv,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation(
                    $"Selected csv file exporter for " +
                    $"media type {acceptHeader}");
                return _serviceProvider.GetService<CsvExporterAluno<T>>();
            }
            else if(string.Equals(acceptHeader,
                MediaTypes.ApplicationPdf,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation(
        $"Selected pdf file exporter for media type {acceptHeader}");
                return _serviceProvider.GetService<PdfExporterAluno<T>>();
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
