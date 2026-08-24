using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.FileExport.Exporter.Contract;
using projetobiblioteca.FileExport.Exporter.Factory;
using System.Globalization;
using System.Text;

namespace projetobiblioteca.FileExport.Exporter.Impl
{
    public class CsvExporterAluno<T> : IFileExporter<T>
    {
        public FileContentResult ExportFile
            (IQueryable<T> list)
        {
            using var memoryStream =
                new MemoryStream();
            using var writer = new StreamWriter(
                memoryStream, Encoding.UTF8,
                leaveOpen: true);
            using var csv = new CsvWriter(writer,
                new CsvConfiguration(
                    CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                });
            csv.WriteRecords(list);
            writer.Flush();
            var fileBytes = memoryStream.ToArray();
            return new FileContentResult(fileBytes,
                MediaTypes.ApplicationCsv)
            {
                FileDownloadName = $"exported_" +
                $"{DateTime.UtcNow:yyyyMMddHHmmss}.csv"
            };

        }
    }
}
