using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCoursesByIdAsync(int id);
        Task<Course> GetCoursesByCourseNoAsync(int courseNumber);
        void Update(Course course);
        void Delete(Course course);
    }
}