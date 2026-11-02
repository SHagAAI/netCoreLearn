using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMovieCRUD.Models;
using Microsoft.EntityFrameworkCore;
using netCore.Data;

namespace NetCoreLearn.Repository
{
    public class DirectorRepository : IDirectorRepository
    {

        private readonly AppDbContext _context;
        public DirectorRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Director>> GetAllDirectorsAsync()
        {
            return await _context.Directors
                .AsNoTracking()
                .Include(d => d.Movies)
                .ToListAsync();
        }

        public async Task<Director?> GetDirectorByIdAsync(int id)
        {
            return await _context.Directors
               .Include(d => d.Movies)
               .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task InsertDirectorAsync(Director director)
        {
            await _context.Directors.AddAsync(director);
        }



        public void UpdateDirectorAsync(Director director)
        {
            _context.Directors.Update(director);
        }


        public async Task DeleteDirectorAsync(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director != null)
            {
                _context.Directors.Remove(director);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}