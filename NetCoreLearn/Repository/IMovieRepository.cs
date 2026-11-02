using System.Collections.Generic;
using System.Threading.Tasks;
using netCore.Models;

namespace NetCoreLearn.Repository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task InsertMovieAsync(Movie movie);
        void UpdateMovie(Movie movie);
        Task DeleteMovieAsync(int id);
        Task SaveChangesAsync();
    }
}
