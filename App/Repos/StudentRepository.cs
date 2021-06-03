using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Repos
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
           _context.Add(student);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsyncById(int id)
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}