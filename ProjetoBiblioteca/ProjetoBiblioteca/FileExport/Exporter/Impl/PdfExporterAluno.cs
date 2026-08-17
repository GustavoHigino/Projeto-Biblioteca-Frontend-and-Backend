using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data;
using projetobiblioteca.FileExport.Exporter.Contract;
using projetobiblioteca.FileExport.Exporter.Factory;
using projetobiblioteca.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace projetobiblioteca.FileExport.Exporter.Impl
{
    public class PdfExporterAluno : IFileExporter<Aluno>
    {
        public FileContentResult ExportFile(IQueryable<Aluno> list)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var pdfBytes = QuestPDF.Fluent.Document.Create(Container =>
            {
                Container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Relatório de Alunos")
                    .SemiBold().FontSize(20).FontColor
                    (QuestPDF.Helpers.Colors.Blue.Medium);
                    page.Content().Padding(10).Table
                    (table =>
                    {
                        var type = typeof(Aluno);
                        var properties =
                        type.GetProperties();
                        table.ColumnsDefinition(columns =>
                        {

                            foreach (var prop in properties)
                            {
                                columns.RelativeColumn();
                            }
                        });
                        table.Header(header =>
                        {
                            foreach (var prop in properties)
                            {
                                header.Cell()
                                .Text(prop.Name).Bold();
                            }
                        });
                        foreach (var alunos in list)
                        {
                            foreach (var prop in properties)
                            {
                                var value = prop
                                .GetValue(alunos);
                                var textValue =
                                value?.ToString() ?? string
                                .Empty;
                                table.Cell().Text(textValue);

                            }
                        }
                    });
                });
            }).GeneratePdf();
            return new FileContentResult(pdfBytes, MediaTypes.ApplicationPdf)
            {
                FileDownloadName = $"alunos_relatorio_" +
                $"{DateTime.UtcNow:yyyyMMddHHmmss}.pdf"
            }; 
            
        }
    }
}
