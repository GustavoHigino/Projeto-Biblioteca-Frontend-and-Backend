using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Funcionario;
using projetobiblioteca.Model;
using projetobiblioteca.Servicos;

namespace projetobiblioteca.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize("Bearer")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ILogger<FuncionarioController> _logger;
        private readonly IFuncionarioService _service;
        public FuncionarioController(ILogger<FuncionarioController> logger,
            IFuncionarioService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> PagedList([FromQuery]int itensPage, [FromQuery] long pageCurrently)
        {
            _logger.LogInformation("Fetch a pagined List ");
            var paginedList= await _service.PagedList(itensPage,pageCurrently);
            if(paginedList==null)
            {
                _logger.LogWarning("list not existent ");
                return BadRequest("Unsuccessfull operation");
            }
            var metadata =
                new
                {
                    paginedList.TotalItens,
                    paginedList.ItensPage,
                    paginedList.TotalPage,
                    paginedList.HasNext,
                    paginedList.HasPreviows
                };
            Response.Headers.Append("x-pagination", JsonConvert
                .SerializeObject(metadata));
            _logger.LogInformation("Showing the list pagined");
            return Ok(paginedList);

        }
        [HttpGet("{id:long}")]
        public IActionResult ShowById([FromRoute]long id)
        {
            _logger.LogInformation("Fetching employee by id");
            var FuncionarioById=_service.FindByIdQuery(id);
            if (FuncionarioById == null)
            {
                _logger.LogInformation("Employee not found or not existent");
                return NotFound("Employee not found or not existent");

            }
            _logger.LogInformation("employee found");
            return Ok(FuncionarioById);
        }
        [HttpPost]
        public IActionResult Add([FromBody] FuncionarioDto employee)
        {
            _logger.LogInformation("trying add an employee");
            var employeeAdd = _service.Add(employee);
            if(employeeAdd == null)
            {
                _logger.LogWarning("employee not add, try again e check your data");
                return BadRequest("employee not add, try again e check your data");
            }
            _logger.LogInformation("employee add succesfully.");
            return Ok(employeeAdd);
        }
        [HttpPut]
        public IActionResult Put([FromBody] FuncionarioDto employee)
        {
            _logger.LogInformation($"trying make an Upgrade in these {employee.Id} employee");
            var employeeUpgrade=_service.Update(employee);
            if(employeeUpgrade == null)
            {
                _logger.LogWarning("Operation unsuccesfull");
                return BadRequest("Operation unsuccesfull");
            }
            _logger.LogInformation("Upgrade done with sucessfully");
            return Ok(employeeUpgrade);
        }
        [HttpPatch("enable/{id:long}")]
        public IActionResult Enable([FromRoute]long id)
        {
            _logger.LogInformation($"trying put on Enable on ID {id}");
            var employeeEnabled=_service.Enable(id);
            if (employeeEnabled == null)
            {
                _logger.LogWarning("attempt no succesfully");
                return BadRequest("attempt no succesfully");
            }
            _logger.LogInformation($"succesfully in put enabled on ID {id}");
            return Ok(employeeEnabled);

        }
        [HttpPatch("disable/{id:long}")]
        public IActionResult Disable([FromRoute] long id)
        {
            _logger.LogInformation($"trying disable an ID {id}");
            var employeeDisable=_service.Disable(id);
            if(employeeDisable == null)
            {
                _logger.LogWarning("Something happen wrong with operation about disable employee");
                return BadRequest("Something happen wrong with operation about disable employee");
            }
            _logger.LogInformation($"Id {id} disable succesfully");
            return Ok(employeeDisable); 
        }
    }
}
