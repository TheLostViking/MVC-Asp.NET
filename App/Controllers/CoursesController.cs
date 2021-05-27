using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace App.Controllers
{
    public class CoursesController : Controller
    {

        //GET /Courses/
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}