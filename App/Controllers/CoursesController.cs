using App.Data;
using App.Entities;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var result = await _context.Courses.ToListAsync();
            return View("Index", result);
        }

        [HttpGet()]
        public IActionResult AddCourse()
        {
            return View("AddCourse");
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(AddCourseViewModel data) 
        {
             var course = new Course
             {
                Title = data.Title,
                Description = data.Description,
                Category = data.Category,
                Length = data.Length,
                Price = data.Price
            };

            //Adding object to EF ChangeTracking
            _context.Courses.Add(course);
            //Saves to the database
            var result = await _context.SaveChangesAsync();            
            return RedirectToAction("Index");      
        }
    }
}