using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repos
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Status> GetStatusAsync(string name)
        {
            return await _context.Statuses.SingleOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
        }
    }
}