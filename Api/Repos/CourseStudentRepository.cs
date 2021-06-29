using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repos
{
    public class CourseStudentRepository : ICourseStudentRepository
    {
        private readonly DataContext _context;

        public CourseStudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CourseStudent courseStudent)
        {
            await _context.CourseStudents.AddAsync(courseStudent);
        }

        public async Task<IEnumerable<CourseStudent>> GetCourseStudentByCourseIdAsync(int id)
        {
            return await _context.CourseStudents
                .Where(c => c.CourseId == id)
                .Include(c => c.Student)
                .Include(c => c.Course)
                    .ThenInclude(c => c.Level)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseStudent>> GetCourseStudentByStudentIdAsync(int id)
        {
            return await _context.CourseStudents
                .Where(c => c.StudentId == id)
                .Include(c => c.Student)
                .Include(c => c.Course)
                    .ThenInclude(c => c.Level)
                .ToListAsync();
        }
    }
}