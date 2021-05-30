using App.Data;
using App.Entities;
using App.Interfaces;
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
        
        private readonly ICourseRepository _courseRepo;

        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        //GET /Courses/
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _courseRepo.GetCoursesAsync();
            return View("Index", result);
        }

        [HttpGet()]
        public IActionResult AddCourse()
        {
            var model = new AddCourseViewModel();
            return View("AddCourse", model);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(AddCourseViewModel data) 
        {
            if(!ModelState.IsValid)
            {
                return View("AddCourse", data);
            }

            var course = new Course
            {
                Title = data.Title,
                Description = data.Description,
                Category = data.Category,
                Length = data.Length,
                Price = (decimal)data.Price
            };

            //Adding object to EF ChangeTracking
            _courseRepo.Add(course);
            //Saves to the database
            if(await _courseRepo.SaveAllAsync()) return RedirectToAction("Index");
            return View("Error");   
        }

        //Method for getting the course to Edit
        [HttpGet()]
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _courseRepo.GetCoursesByIdAsync(id);
            var model =  new EditCourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Length = course.Length,
                Category = course.Category,
                Price = course.Price
            };
            return View("EditCourse", model);
        }

        //Method for updating the course with new parameters, saves to Database
        [HttpPost()]
        public async Task<IActionResult> EditCourse(EditCourseViewModel data)
        {   
            var course = await _courseRepo.GetCoursesByIdAsync(data.Id);

            course.Title = data.Title;
            course.Description = data.Description;
            course.Length = data.Length;
            course.Category = data.Category;
            course.Price = data.Price;

            _courseRepo.Update(course);

            if(await _courseRepo.SaveAllAsync()) return RedirectToAction("Index");
            return View("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetCoursesByIdAsync(id);
            _courseRepo.Delete(course);

            if(await _courseRepo.SaveAllAsync()) return RedirectToAction("Index");
            return View("Error");

        }
    }
}