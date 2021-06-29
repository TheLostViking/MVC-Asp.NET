using System.Threading.Tasks;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class BuyCoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        public BuyCoursesController(ICourseService courseService, IStudentService studentService)
        {
            _courseService = courseService;
            _studentService = studentService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _courseService.GetCoursesAsync();

            return View("Index", result);
        }
    }
}