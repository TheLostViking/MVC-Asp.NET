using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ILevelRepository
    {
        Task<Level> GetLevelAsync(int id);
        Task<Level> GetLevelAsync(string name);
        Task<IEnumerable<Level>> GetLevelsAsync();
    }
}