using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> GetCourseByCourseNumberAsync(int courseNumber);
        Task<IEnumerable<Course>> GetCoursesByStatusAsync(string status);
        void SetActiveAsync (int id);
        void SetInavticeAsync (int id);
        void Update(Course course);
        void Delete(Course course);
    }
}