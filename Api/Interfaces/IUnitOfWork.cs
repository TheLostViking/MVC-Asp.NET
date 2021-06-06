using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        IStudentRepository StudentRepository { get; }
        Task<bool> Complete(); // Take cars of all save actions
        bool HasChanges(); // Tracks changes
    }
}