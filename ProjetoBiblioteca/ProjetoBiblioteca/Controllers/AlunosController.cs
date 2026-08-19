using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Aluno;
using projetobiblioteca.HATEOAS.Filters;
using projetobiblioteca.Model;
using projetobiblioteca.Servicos;

namespace ProjetoBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("Bearer")]
public class AlunosController : ControllerBase
{
    private readonly ILogger<AlunosController> _logger;
    private readonly IAlunoService _alunoService;

    public AlunosController(ILogger<AlunosController> logger,
        IAlunoService alunoService)
    {
        _alunoService = alunoService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> ShowPagined([FromQuery]int itensPage,[FromQuery]long pageCurrently)
    {
        _logger.LogInformation("Fetching a Ordened list");
        var pagedList=  await _alunoService.PagedList(itensPage, pageCurrently);
        if (pagedList==null) 
        {
            _logger.LogWarning("Non-exist");
            return NotFound("Non-exist");

        }
        _logger.LogInformation("Showing the list");
        return Ok(pagedList);

    }
    [HttpGet("{id:long}")]
    public IActionResult ShowById([FromRoute] long id)
    {
        
        _logger.LogInformation("fetching student by ID");
        var student=_alunoService.ShowById(id);
        if (student == null)
        {
            _logger.LogWarning("student non-existent");
            return NotFound("student non-existent");
        }
        _logger.LogInformation("Showing student by id below");
        return Ok(student);
    }
    [HttpPost]
    public IActionResult AddNewStudents([FromBody]AlunoDto aluno)
    {

        if(aluno == null)
        {
            _logger.LogWarning("Student is null");
            return NotFound("Student is null");
        }
        var student=_alunoService.Add(aluno);

        _logger.LogInformation($"{student.Id}: Student = {student.Nome} Add. ");
        return Ok(student);
    }
    [HttpPut]
    public IActionResult UpgradeStudentsById([FromBody]AlunoDto aluno)
    {
        var student = _alunoService.Update(aluno);
        if(student == null)
        {
            _logger.LogWarning("Student not found find another id.");
            return NotFound("Student not found");
        }
        _logger.LogInformation("Update done with succesfully");
        return Ok(student);
    }
    [HttpPatch("enable/{id:long}")]
    public IActionResult PatchEnableStudantsById([FromRoute]long id)
    {
        var student=_alunoService.Enable(id);
        if (student == null)
        {
            _logger.LogWarning("Student not found maybe your id not exist.");
            return NotFound("Student not found maybe your id not exist.");
        }
        _logger.LogInformation($"Student {student.Id}, {student.Nome} update for Enable with succesfully");
        return Ok(student);
    }
    [HttpPatch("disable/{id:long}")]
    public IActionResult PatchDisableStudantsById([FromRoute]long id)
    {
        var student=_alunoService.Disable(id);
        if (student == null)
        {
            _logger.LogWarning("Student not found maybe your id not exist.");
            return NotFound("Student not found maybe your id not exist.");
        }
        _logger.LogInformation($"Student {student.Id}, {student.Nome} update for Disable with succesfully");
        return Ok(student);
    }
}
