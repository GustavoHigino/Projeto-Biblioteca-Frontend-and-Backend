using Microsoft.AspNetCore.Mvc;

namespace ProjetoBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunosController : ControllerBase
{
    private readonly ILogger<AlunosController> _logger;

    public AlunosController()
    {
        
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();

    }
}
