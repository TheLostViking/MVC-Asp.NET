using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repos
{
    public class LevelRepository : ILevelRepository
    {
        private readonly DataContext _context;

        public LevelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Level> GetLevelAsync(int id)
        {
            return await _context.Levels.SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Level> GetLevelAsync(string name)
        {
            return await _context.Levels.SingleOrDefaultAsync(l => l.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Level>> GetLevelsAsync()
        {
            return await _context.Levels.ToListAsync();
        }
    }
}