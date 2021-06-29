using System.Collections.Generic;
using System.Linq;
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

        public async Task<Course> GetCourseByCourseNumberAsync(int courseNumber)
        {
            return await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNumber);
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByStatusAsync(string status)
        {
            return await _context.Courses
                .Where(c => c.Status.Name.ToLower() == status.ToLower())
                .Include(c => c.Level)
                .Include(c => c.Status)
                .Include(c => c.CourseStudents)
                    .ThenInclude(c => c.Student)
                .OrderBy(c => c.Status.Name)
                    .ThenBy(c => c.CourseNumber)
                .ToListAsync();
        }

        public async void SetActiveAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            course.Status = await _context.Statuses.SingleOrDefaultAsync(s => s.Name == "Active");
        }

        public async void SetInavticeAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            course.Status = await _context.Statuses.SingleOrDefaultAsync(s => s.Name == "Inactive");
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}