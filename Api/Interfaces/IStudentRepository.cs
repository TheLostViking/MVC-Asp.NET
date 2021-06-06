using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IStudentRepository
    {
        Task AddStudent(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        void Update(Student student);
        void Delete(Student student);
        Task<bool> SaveAllChanges();
    }
}