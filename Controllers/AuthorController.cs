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
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _repo;

        public AuthorController(IRepository<Author> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Author>> Get(int id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpGet("filter"), AllowAnonymous]
        public async Task<ActionResult<List<Author>>> Get([FromQuery] AuthorFilter filters)
        {
            return Ok(await _repo.Get(filters));
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Author>> Post(Author entity)
        {
            return CreatedAtAction("Post", await _repo.Post(entity));
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Author>> Put(Author entity)
        {
            var result = await _repo.Put(entity);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Author>> Delete(Author entity)
        {
            var result = await _repo.Delete(entity.Id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }
    }
}
