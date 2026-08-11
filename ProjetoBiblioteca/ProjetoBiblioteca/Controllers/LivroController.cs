using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data;
using projetobiblioteca.Servicos;

namespace projetobiblioteca.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize("Bearer")]
    public class LivroController : ControllerBase
    {
        private readonly ILogger<LivroController> _logger;
        private readonly ILivroService _service;
        public LivroController(ILogger<LivroController> logger,
            ILivroService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPagined([FromQuery]int itensPage,[FromQuery]long pageCurrently)
        {
            _logger.LogInformation("trying to fetch books");
            var paginedList=_service.PagedList(itensPage,pageCurrently);
            if (paginedList == null)
            {
                _logger.LogWarning("Something wrong in to fetch books");
                return NotFound("Books not found check your code");
            }
            _logger.LogInformation("Books found successfully");
            return Ok(paginedList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]long id)
        {
            _logger.LogInformation($"Fetching a book by Id {id}");

            var livroById = _service.ShowById(id);
            if(livroById == null)
            {
                _logger.LogInformation($"Fetch Unsuccessfully by Id {id}");
                return NotFound($"Fetch Unsuccessfully by Id {id}");
            }
            _logger.LogInformation("book found successfully");
            return Ok(livroById);
        }
        [HttpPost]
        public IActionResult Add([FromBody]Livro book)
        {
            _logger.LogInformation("trying add a book in a dataBase");
            var livroAdd = _service.Add(book);
            if (livroAdd == null)
            {
                _logger.LogWarning("Error in add the book on the database check your data");
                return BadRequest("Error in add the book on the database check your data");
            }
            _logger.LogInformation("book add successfully");
            return Ok(livroAdd);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Livro book)
        {
            _logger.LogInformation("attempt update a book");
            var bookUpgrade= _service.Update(book);
            if(bookUpgrade == null)
            {
                _logger.LogWarning("Error update not work this time try again");
                return BadRequest("Error update not work this time try again");
            }
            _logger.LogInformation("book update with succesfully");
            return Ok(bookUpgrade);
        }
        [HttpPatch("enable/{id:long}")]
        public IActionResult Enable([FromRoute]long id)
        {
            _logger.LogInformation("attempt enable a book");
            var bookEnabled = _service.Enable(id);
            if(bookEnabled == null)
            {
                _logger.LogWarning("Operation to enable a book uncessfully");
                return NotFound("Book Not found and your operation no happen");
            }
            _logger.LogInformation("book modified for Enabled with Succesfully");
            return Ok(bookEnabled);
        }
        [HttpPatch("disable/{id:long}")]
        public IActionResult Disable([FromRoute]long  id)
        {
            _logger.LogInformation("trying modified Enable to Disable");
            var bookDisabled = _service.Disable(id);
            if(bookDisabled == null)
            {
                _logger.LogWarning("attempt for modiefied enable to disable was unsuccessfully");
                return BadRequest("attempt for modiefied enable to disable was unsuccessfully");
            }
            _logger.LogInformation("book modified for Enabled with Succesfully");
            return Ok(bookDisabled);
        }
    }
}
