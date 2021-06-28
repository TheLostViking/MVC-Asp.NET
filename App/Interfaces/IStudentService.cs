using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetStudentsAsync();
        Task<StudentModel> GetStudentByIdAsync(int id);
        Task<StudentModel> GetStudentByEmailAsync(string email);
        Task<bool> AddStudent(StudentModel model);
        Task<bool> EditStudent(int id, StudentModel model);
        Task<bool> DeleteStudent(int id);
    }
}