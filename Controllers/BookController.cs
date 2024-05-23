using DAL;
using DAL.Filters;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EPSI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _repo;

        public BookController(IRepository<Book> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Book>> Get(int id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpGet("filter"), AllowAnonymous]
        public async Task<ActionResult<List<Book>>> Get([FromQuery] BookFilter filters)
        {
            return Ok(await _repo.Get(filters));
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Book>> Post(Book entity)
        {
            return CreatedAtAction("Post", await _repo.Post(entity));
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Book>> Put(Book entity)
        {
            var result = await _repo.Put(entity);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Book>> Delete(Book entity)
        {
            var result = await _repo.Delete(entity.Id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }
    }
}
