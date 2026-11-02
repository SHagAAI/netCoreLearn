using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMovieCRUD.Models;

namespace NetCoreLearn.Repository
{
    public interface IDirectorRepository
    {
        Task<IEnumerable<Director>> GetAllDirectorsAsync();
        Task<Director?> GetDirectorByIdAsync(int id);
        Task InsertDirectorAsync(Director director);
        void UpdateDirectorAsync(Director director);
        Task DeleteDirectorAsync(int id);
        Task SaveChangesAsync();                          // Persists changes to the database
    }
}