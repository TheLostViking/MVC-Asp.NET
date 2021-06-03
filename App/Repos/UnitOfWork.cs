using System.Threading.Tasks;
using App.Data;
using App.Interfaces;

namespace App.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICourseRepository CourseRepository => new CourseRepository(_context);

        public IStudentRepository StudentRepository => new StudentRepository(_context);

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}