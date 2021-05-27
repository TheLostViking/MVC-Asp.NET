using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet()]
        public IActionResult AddCourse()
        {
            return View("AddCourse");
        }

        [HttpPost()]
        public IActionResult AddCourse(AddCourseViewModel data) 
        {
            return RedirectToAction("Index");      
        }
    }
}