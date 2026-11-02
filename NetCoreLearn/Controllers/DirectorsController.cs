using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netCore.DTOs;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace netCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: api/directors
        [HttpGet]
        public async Task<IActionResult> GetAllDirector()
        {
            var result = await _directorService.GetAllDirectorAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // GET: api/directors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectorById(int id)
        {
            var result = await _directorService.GetDirectorByIdAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // POST: api/directors
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDirector(CreateDirectorDto newDirectorDto)
        {
            var result = await _directorService.CreateDirectorAsync(newDirectorDto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return CreatedAtAction(nameof(GetDirectorById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/directors/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirector(int id, UpdateDirectorDto updateDirectorDto)
        {
            var result = await _directorService.UpdateDirectorAsync(id, updateDirectorDto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(result.Data);
        }

        // DELETE: api/directors/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var result = await _directorService.DeleteDirectorAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.Message);
            }
            return Ok(new { message = result.Message });
        }
    }
}
