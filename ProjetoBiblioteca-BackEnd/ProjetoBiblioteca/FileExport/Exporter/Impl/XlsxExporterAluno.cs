using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data;
using projetobiblioteca.FileExport.Exporter.Contract;
using projetobiblioteca.FileExport.Exporter.Factory;

namespace projetobiblioteca.FileExport.Exporter.Impl
{
    public class XlsxExporterAluno<T> : IFileExporter<T>
    {
        public FileContentResult ExportFile(IQueryable<T> list)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets
                .Add("Aluno");
            var t = typeof(T);
            var properties = t.GetProperties().
                Where(p => p.PropertyType ==
                typeof(string) ||
                !typeof(System.Collections.IEnumerable)
                .IsAssignableFrom(p.PropertyType)
                ).ToArray();
            for(int i=0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value
                    = properties[i].Name;
            }
            var headerRange = worksheet.Range
                (1, 1, 1,
                properties.Length);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;
            int row = 2;
            foreach (var entity in list)
            {
                for(int col =0; col<properties.Length;
                    col++)
                {
                    var value = properties[col]
                        .GetValue(entity);
                    if(value is bool boolVal)
                    {
                        worksheet.Cell(row, col + 1)
                            .Value = boolVal ? "Yes" : "No";
                    }
                    else
                    {
                        worksheet.Cell(
                            row, col + 1).Value=value?
                            .ToString() ?? string.Empty;
                    }
                    
                }
                row++;
            }
            worksheet.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var fileBytes = stream.ToArray();
            return new FileContentResult(
                fileBytes,
                MediaTypes.ApplicationXlsx)
            {
                FileDownloadName = $"Aluno_exported_" +
                $"{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx"
            };
        }
    }
}
