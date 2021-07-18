using App.Entities;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _service;

        public CoursesController(IUnitOfWork unitOfWork, ICourseService service)
        {
            _service = service;
        }

        //GET /Courses/
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetCoursesAsync();
            return View("Index", result);
        }

        [HttpGet()]
        public async Task<IActionResult> AddCourse()
        {        
        //     var levels = await _service.GetLevelsAsync();        
        //     var courseModel = new AddCourseViewModel();
        //     courseModel.LevelCollection = levels;     
            return View("AddCourse");
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(AddCourseViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCourse", data);
            }

            var course = new CourseModel
            {
                Title = data.Title,
                CourseNumber = data.CourseNumber,
                Description = data.Description,
                Level = data.Level,
                Length = data.Length,
                Price = (decimal)data.Price
            };

            try
            {
                if (await _service.AddCourse(course))
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Error");
        }

        //Method for getting the course to Edit
        [HttpGet()]
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _service.GetCourseByIdAsync(id);

            var model = new EditCourseViewModel
            {
                Id = course.CourseId,
                CourseNumber = course.CourseNumber,
                Title = course.Title,
                Description = course.Description,
                Length = course.Length,
                Level = course.Level,
                Price = course.Price
            };

            return View("EditCourse", model);
        }

        //Method for updating the course with new parameters, saves to Database
        [HttpPost()]
        public async Task<IActionResult> EditCourse(EditCourseViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCourse", data);
            }

            var course = await _service.GetCourseByIdAsync(data.Id);

            course.Title = data.Title;
            course.Description = data.Description;
            course.Length = data.Length;
            course.Level = data.Level;
            course.Price = data.Price;

            try
            {
                if (await _service.EditCourse(course.CourseId, course)) return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Error");
        }

        public async Task<IActionResult> Delete(int courseNumber)
        {
            var course = await _service.GetCourseByCourseNoAsync(courseNumber);

            if (await _service.DeleteCourse(course.CourseNumber)) return RedirectToAction("Index");
            return View("Error");
        }
    }
}