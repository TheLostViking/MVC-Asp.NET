using System.Threading.Tasks;
using Api.Data;
using Api.Interfaces;

namespace Api.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICourseRepository CourseRepository => new CourseRepository(_context);
        public IStatusRepository StatusRepository => new StatusRepository(_context);
        public IStudentRepository StudentRepository => new StudentRepository(_context);
        public ILevelRepository LevelRepository => new LevelRepository(_context);
        public ICourseStudentRepository CourseStudentRepository => new CourseStudentRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}