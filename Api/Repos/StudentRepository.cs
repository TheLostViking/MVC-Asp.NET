using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repos
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public async Task<Student> GetStudentByEmailAsync(string email)
        {
            return await _context.Students
            .Include(c => c.CourseStudents)
                .ThenInclude(c => c.Course)
                    .ThenInclude(c => c.Status)
            .SingleOrDefaultAsync(c => c.Email.ToUpper() == email.ToUpper());
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(c => c.CourseStudents)
                    .ThenInclude(c => c.Course)
                        .ThenInclude(c => c.Status)
                .SingleOrDefaultAsync(c => c.StudentId == id);           
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students
                .Include(c => c.CourseStudents)
                    .ThenInclude(c => c.Course)
                        .ThenInclude(c => c.Status)
                .OrderBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                .ToListAsync();
                         
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}