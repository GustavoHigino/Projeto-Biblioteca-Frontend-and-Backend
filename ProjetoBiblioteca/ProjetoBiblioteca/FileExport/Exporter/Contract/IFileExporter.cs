using Microsoft.AspNetCore.Mvc;

namespace projetobiblioteca.FileExport.Exporter.Contract
{
    public interface IFileExporter<T>
    {
        FileContentResult ExportFile(IQueryable<T> list);
    }
}
