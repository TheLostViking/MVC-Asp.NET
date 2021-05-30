using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;

namespace App.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCoursesByIdAsync(int id);
        void Update(Course course);
        void Delete(Course course);
        Task<bool> SaveAllAsync();
    }
}