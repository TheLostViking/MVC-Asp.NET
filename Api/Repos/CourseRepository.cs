using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

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
           _context.Courses.Remove(course);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCoursesByCourseNoAsync(int courseNumber)
        {
            return await _context.Courses.FindAsync();
        }

        public async Task<Course> GetCoursesByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}