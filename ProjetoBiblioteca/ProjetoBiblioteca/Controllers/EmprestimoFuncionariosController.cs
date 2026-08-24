using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data.DTO.EmprestimoFuncionario;
using projetobiblioteca.FileExport.Exporter.Factory;
using projetobiblioteca.Model;
using projetobiblioteca.Servicos;

namespace projetobiblioteca.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize("Bearer")]
    public class EmprestimoFuncionariosController : ControllerBase
    {
        private readonly ILogger<EmprestimoFuncionariosController> _logger;
        private readonly IEmprestimoFuncionarioService _service;
        private readonly FileExporterFactory<EmprestimoFuncionario> _exporter;
        public EmprestimoFuncionariosController(ILogger<EmprestimoFuncionariosController> logger,
            IEmprestimoFuncionarioService service,
            FileExporterFactory<EmprestimoFuncionario> exporter)
        {
            _exporter = exporter;
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> PaginationList([FromQuery]int itensPage,[FromQuery]long pageCurrently)
        {
            _logger.LogInformation("feching a pagination list");
            var listPagination=await _service.PagedList(itensPage, pageCurrently);
            if (listPagination == null)
            {
                _logger.LogWarning("the pagination list not found");
                return NotFound("the pagination list not found");
            }
            _logger.LogInformation("the pagination list was found and it was show");
            return Ok(listPagination);

        }
        [HttpGet("{id:long}")]
        public IActionResult GetbyId([FromRoute]long id)
        {
            _logger.LogInformation("fetching an loan student by ID");
            var emprestimoFuncionarioById=_service.FindByIdQuery(id);
            if (emprestimoFuncionarioById == null)
            {
                _logger.LogWarning("loan student not found");
                return NotFound("loan student not found");
            }
            _logger.LogInformation("fetching by a loan student has been succesfully");
            return Ok(emprestimoFuncionarioById);
        }
        [HttpPost]
        public IActionResult EmprestimoFuncionarioAdd([FromBody]EmprestimoFuncionarioDto emprestimoFuncionario)
        {
            _logger.LogInformation("trying add a loan student");
            var emprestimoFuncionarioAdd = _service.Add(emprestimoFuncionario);
            if (emprestimoFuncionarioAdd == null)
            {
                _logger.LogWarning("Failure in add a loan student");
                return BadRequest("Failure in add a loan student");
            }
            _logger.LogInformation("add a loan student with succesfully");
            return Ok(emprestimoFuncionarioAdd);
        }
        [HttpPut]
        public IActionResult EmprestimoFuncionarioUpdate([FromBody]EmprestimoFuncionarioDto emprestimoFuncionario)
        {
            _logger.LogInformation("trying modified a loan student");
            var emprestimoFuncionarioUpdate = _service.Update(emprestimoFuncionario);
            if (emprestimoFuncionarioUpdate == null)
            {
                _logger.LogWarning("Failure in modified a loan student");
                return BadRequest("Failure in modified a loan student");
            }
            _logger.LogInformation("loan student modified with succesfully");
            return Ok(emprestimoFuncionarioUpdate);
        }
        [HttpPatch("{id:long}")]
        public IActionResult EmprestimoFuncionarioReturned([FromRoute]long id)
        {
            _logger.LogInformation("trying modifie loan student as returned");
            var emprestimoFuncionarioReturned = _service.Returned(id);
            if(emprestimoFuncionarioReturned == null)
            {
                _logger.LogWarning("failure in modifie the loan student as returned");
                return NotFound("failure in modifie the loan student as returned");
            }
            _logger.LogInformation("loan student modified successfully");
            return Ok(emprestimoFuncionarioReturned);
        }
        [HttpPatch("{id:long}")]
        public IActionResult EmprestimoFuncionarioNotReturned([FromRoute] long id)
        {
            _logger.LogInformation("trying modifie loan student as not returned");
            var emprestimoFuncionarioNotReturned = _service.NotReturned(id);
            if (emprestimoFuncionarioNotReturned == null)
            {
                _logger.LogWarning("failure in modifie the loan student as not returned");
                return NotFound("failure in modifie the loan student as not returned");
            }
            _logger.LogInformation("loan student modified successfully");
            return Ok(emprestimoFuncionarioNotReturned);
        }
        [HttpGet("Export")]
        public IActionResult Export
        ([FromHeader(Name = "Accept")] string acceptHeader)
        {
            var query = _service.Show();
            var exporter = _exporter.GetExporter(acceptHeader);
            return exporter.ExportFile(query);
        }
        [HttpGet("Pix/{id:long}")]
        [Produces("image/png")]
        public IActionResult CriarPix(long id)
        {
            var emprestimo = _service.ShowById(id);
            string base64=_service.CriarPix
                 (emprestimo.ValorMulta.ToString(),
                 emprestimo.IdFuncionario);
            if (base64.Contains(","))
            {
                base64=base64.Split(',')[1];
            }
            byte[] imageBytes=Convert.FromBase64String(base64);
            return File(imageBytes, "image/png");
        }

    }
}
