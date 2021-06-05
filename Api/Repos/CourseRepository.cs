using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;

namespace Api.Repos
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Course course)
        {
           await _context.Courses.AddAsync(course);
        }

        public void Delete(Course course)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetCoursesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Course> GetCoursesByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Course course)
        {
            throw new System.NotImplementedException();
        }
    }
}