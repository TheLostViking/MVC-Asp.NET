using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        IStatusRepository StatusRepository { get; }
        IStudentRepository StudentRepository { get; }
        ILevelRepository LevelRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get;}
        Task<bool> Complete(); // Take cares of all save actions
        bool HasChanges(); // Tracks changes
    }
}