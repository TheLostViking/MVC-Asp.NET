using App.Data;
using App.Entities;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;

namespace App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

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
             var course = new Course
             {
                Title = data.Title,
                Description = data.Description,
                Category = data.Category,
                Length = data.Length,
                Price = data.Price
            };

            _context.Courses.Add(course);
            var result = _context.SaveChanges();           
            
            return RedirectToAction("Index");      
        }
    }
}