using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        IStudentRepository StudentRepository { get; }

        Task<bool> SaveAll();
        bool HasChanges();
    }
}