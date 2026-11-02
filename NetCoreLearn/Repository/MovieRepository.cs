using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using netCore.Data;
using netCore.Models;

namespace NetCoreLearn.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Genres)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
