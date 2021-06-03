using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;

namespace App.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Task<IEnumerable<Student>> GetStudentsAsyncById(int id);

    }
}