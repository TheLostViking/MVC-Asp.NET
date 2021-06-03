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
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET /Courses/
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.CourseRepository.GetCoursesAsync();
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
            _unitOfWork.CourseRepository.Add(course);
            //Saves to the database
            if(await _unitOfWork.SaveAll()) return RedirectToAction("Index");
            return View("Error");   
        }

        //Method for getting the course to Edit
        [HttpGet()]
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCoursesByIdAsync(id);
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
            var course = await _unitOfWork.CourseRepository.GetCoursesByIdAsync(data.Id);

            course.Title = data.Title;
            course.Description = data.Description;
            course.Length = data.Length;
            course.Category = data.Category;
            course.Price = data.Price;

            _unitOfWork.CourseRepository.Update(course);

            if(await _unitOfWork.SaveAll()) return RedirectToAction("Index");
            return View("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCoursesByIdAsync(id);
            _unitOfWork.CourseRepository.Delete(course);

            if(await _unitOfWork.SaveAll()) return RedirectToAction("Index");
            return View("Error");

        }
    }
}