using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ILevelRepository
    {
        Task<Level> GetLevelByIdAsync(int id);
        Task<Level> GetLevelByNameAsync(string name);
        Task<IEnumerable<Level>> GetLevelsAsync();
    }
}