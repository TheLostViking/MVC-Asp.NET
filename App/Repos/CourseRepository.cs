using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Repos
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<Course> GetCourseByCourseNumberAsync(int courseNumber)
        {
            return await _context.Courses.FindAsync(courseNumber);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCoursesByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }     

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}