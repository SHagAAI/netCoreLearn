using System.Collections.Generic;
using System.Threading.Tasks;
using netCore.Models;

namespace NetCoreLearn.Repository
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task InsertGenreAsync(Genre genre);
        void UpdateGenre(Genre genre);
        Task DeleteGenreAsync(int id);
        Task SaveChangesAsync();
    }
}
