using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netCore.DTOs;
using Services;

namespace netCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var result = await _movieService.GetAllMoviesAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // POST: api/movies
        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDto dto)
        {
            var result = await _movieService.CreateMovieAsync(dto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return CreatedAtAction(nameof(GetMovieById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDto dto)
        {
            var result = await _movieService.UpdateMovieAsync(id, dto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(new { message = result.Message });
        }
    }
}
