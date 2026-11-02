using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netCore.DTOs;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace netCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/genres
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _genreService.GetAllGenresAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // GET: api/genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var result = await _genreService.GetGenreByIdAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // POST: api/genres
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto dto)
        {
            var result = await _genreService.CreateGenreAsync(dto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return CreatedAtAction(nameof(GetGenreById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/genres/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto dto)
        {
            var result = await _genreService.UpdateGenreAsync(id, dto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // DELETE: api/genres/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _genreService.DeleteGenreAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(new { message = result.Message });
        }
    }
}
