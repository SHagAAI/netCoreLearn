using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCore.Data;
using netCore.DTOs;
using netCore.Models;

namespace netCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GenresController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }

    // GET: api/genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Movies)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return NotFound($"Genre with ID {id} was not found.");
        }

        return genre;
    }

    // POST: api/genres
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre(CreateGenreDto newGenreDto)
    {
        // var genre = new Genre
        // {
        //     Name = dto.Name
        // };

        var genre = _mapper.Map<Genre>(newGenreDto);

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    // PUT: api/genres/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto updateGenreDto)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound($"Genre with ID {id} was not found.");
        }

        // genre.Name = dto.Name;
        _mapper.Map(updateGenreDto, genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/genres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound($"Genre with ID {id} was not found.");
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
