using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data.DTO.EmprestimoAluno;
using projetobiblioteca.Model;
using projetobiblioteca.Servicos;

namespace projetobiblioteca.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize("Bearer")]
    public class EmprestimoAlunosController : ControllerBase
    {
        private readonly ILogger<EmprestimoAlunosController> _logger;
        private readonly IEmprestimoAlunoService _service;
        public EmprestimoAlunosController(ILogger<EmprestimoAlunosController> logger,
            IEmprestimoAlunoService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> ListPagined([FromQuery]int itensPage,[FromQuery] long pageCurrently)
        {
            _logger.LogInformation("fetching a list pagined");
            var listPagined = await _service.PagedList(itensPage, pageCurrently);
            if (listPagined == null)
            {
                _logger.LogWarning("list pagined not found");
                return NotFound("list pagined not found");
            }
            _logger.LogInformation("showing the list pagined");
            return Ok(listPagined);
        }
        [HttpGet("{id:long}")]
        public IActionResult GetById([FromRoute]long id)
        {
            _logger.LogInformation("fetching a employee loam by Id");
            var getById = _service.FindByIdQuery(id);
            if (getById == null)
            {
                _logger.LogWarning("employee loan not found");
                return NotFound("employee loan not found");
            }
            _logger.LogInformation("employee loan found");
            return Ok(getById);
        }
        [HttpPost]
        public IActionResult Add([FromBody] EmprestimoAlunoDto emprestimoAlunos)
        {
            _logger.LogInformation("add an employee loan");
            var emprestimoAlunosAdd = _service.Add(emprestimoAlunos);
            if (emprestimoAlunosAdd == null)
            {
                _logger.LogWarning("failure add an employee loan");
                return BadRequest("failure add an employee loan");
            }
            _logger.LogInformation("employee loan done with successfully");
            return Ok(emprestimoAlunosAdd);
        }
        [HttpPut]
        public IActionResult Update([FromBody] EmprestimoAlunoDto emprestimoAluno)
        {
            _logger.LogInformation("modifing some data about the employee loan");
            var emprestimoAlunosUpdate = _service.Update(emprestimoAluno);
            if (emprestimoAlunosUpdate == null)
            {
                _logger.LogWarning("Error modified an employee loan");
                return BadRequest("Error modified an employee loan");
            }
            _logger.LogInformation("the modified has been successfull");
            return Ok(emprestimoAlunosUpdate);
        }
        [HttpPatch("{id:long}")]
        public IActionResult Returned([FromRoute]long id) 
        {
            _logger.LogInformation("fetching about the id of the loan employee for modifie");
            var emprestimoReturned = _service.Returned(id);
            if (emprestimoReturned == null)
            {
                _logger.LogWarning("modified for enable has been unsuccesfully");
                return NotFound("modified for enable has been unsuccesfully") ;
            }
            _logger.LogInformation("the modification to enable has been succesfully");
            return Ok(emprestimoReturned);
        }
        [HttpPatch("{id:long}")]
        public IActionResult NotReturned([FromRoute]long id)
        {
            _logger.LogInformation("fetching about the id of the loan employee for modifie");
            var emprestimoNotReturned = _service.NotReturned(id);
            if (emprestimoNotReturned == null)
            {
                _logger.LogWarning("modified for disable has been unsuccesfully");
                return NotFound("modified for disable has been unsuccesfully");
            }
            _logger.LogInformation("the modification to disable has been succesfully");
            return Ok(emprestimoNotReturned);
        }
    }
}
