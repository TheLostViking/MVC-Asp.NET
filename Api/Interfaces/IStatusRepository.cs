using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IStatusRepository
    {
                Task<Status> GetStatusAsync(string name);
    }
}