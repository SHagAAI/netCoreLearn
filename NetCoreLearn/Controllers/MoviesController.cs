using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCore.Data;
using netCore.DTOs;
using netCore.Models;

namespace netCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MoviesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _context.Movies
            .Include(m => m.Director)
            .Include(m => m.Genres)
            .ToListAsync();
    }

    // GET: api/movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Director)
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        return movie;
    }

    // POST: api/movies
    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie(CreateMovieDto newMovieDto)
    {
        var director = await _context.Directors.FindAsync(newMovieDto.DirectorId);
        if (director == null)
        {
            return BadRequest($"Director with ID {newMovieDto.DirectorId} does not exist.");
        }


        var genres = await _context.Genres
            .Where(g => newMovieDto.GenreIds.Contains(g.Id))
            .ToListAsync();

        // Creates a brand new object (Used in POST)
        var movie = _mapper.Map<Movie>(newMovieDto);
        movie.Genres = genres;

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    // PUT: api/movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDto updateMovieDto)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        var directorExists = await _context.Directors.AnyAsync(d => d.Id == updateMovieDto.DirectorId);
        if (!directorExists)
        {
            return BadRequest($"Director with ID {updateMovieDto.DirectorId} does not exist.");
        }

        var genres = await _context.Genres
            .Where(g => updateMovieDto.GenreIds.Contains(g.Id))
            .ToListAsync();


        // Overwrites properties on an existing object
        _mapper.Map(updateMovieDto, movie);

        movie.Genres = genres;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
