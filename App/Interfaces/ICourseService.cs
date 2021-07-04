using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;
using App.ViewModels;

namespace App.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetCoursesAsync();
        Task<List<CourseModel>> GetActiveCoursesAsync();
        Task<CourseModel> GetCourseByIdAsync(int id);
        Task<CourseModel> GetCourseByCourseNoAsync(int courseNumber);
        Task<List<LevelModel>> GetLevelsAsync(); 
        Task<bool> SetCourseAsInactiveAsync(int id);       
        Task<bool> AddCourse(CourseModel model);
        Task<bool> EditCourse(int id, CourseModel model);
        Task<bool> DeleteCourse(int courseNumber);
    }
}