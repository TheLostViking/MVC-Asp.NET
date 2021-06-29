using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>().HasKey(cs => new { cs.CourseId, cs.StudentId }); 

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasData(
                    new Level() { Id = 1, Name = "Beginner" },
                    new Level() { Id = 2, Name = "Intermediate" },
                    new Level() { Id = 3, Name = "Advanced" }
                );
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasData(
                    new Status() { Id = 1, Name = "Active" },
                    new Status() { Id = 9, Name = "Inactive" }
                );
            });
        }
    }
}