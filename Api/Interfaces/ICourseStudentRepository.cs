using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseStudentRepository
    {
        Task AddAsync(CourseStudent courseStudent);
        Task<IEnumerable<CourseStudent>> GetCourseStudentByStudentIdAsync(int id);
        Task<IEnumerable<CourseStudent>> GetCourseStudentByCourseIdAsync(int id);  
    }
}