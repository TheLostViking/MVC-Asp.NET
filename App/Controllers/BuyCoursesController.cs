using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using App.ViewModels;
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
        public async Task<IActionResult> Index(string studentEmail)
        {
            var student = await _studentService.GetStudentByEmailAsync(studentEmail);
            if (studentEmail != null)
            {
                var model = new DetailStudentCourseViewModel
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    Adress = student.Adress,
                    Courses = student.Courses,
                    AvailableCourses = new List<CourseModel>()
                };

                var courses = await _courseService.GetActiveCoursesAsync();
                foreach (var c in courses)
                {
                    if (!IsCourseAvailable(model.Courses, c))
                    {
                        model.AvailableCourses.Add(c);
                    }
                }

                return View("Index", model);
            }
            return Content("Email not found!");
        }

        [HttpGet()]
        public async Task<IActionResult> AddCourseToStudent(int studentId, int courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (student != null && course != null)
            {
                var model = new BuyNewCourseViewModel()
                {
                    CourseId = course.CourseId,
                    CourseNumber = course.CourseNumber,
                    Title = course.Title,
                    Description = course.Description,
                    Length = course.Length,
                    Price = course.Price,
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                };
                return View("AddCourseToStudent", model);
            }
            return Content($"Could not find studentID {studentId} or courseID {courseId}");
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourseToStudent(int studentId, int courseId, BuyNewCourseViewModel data)
        {
            // if(!ModelState.IsValid) return View("AddCourseToStudent", data);

            var course = await _courseService.GetCourseByIdAsync(courseId);
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (course != null && student != null)
            {
                var courseStudent = new CourseStudentModel
                {
                    StudentId = student.StudentId,
                    CourseId = course.CourseId
                };

                try
                {
                    if (await _studentService.AddCourseToStudentAsync(student.StudentId, course.CourseId, courseStudent))
                        return RedirectToAction("Index", "Students");
                }
                catch (Exception ex)
                {
                    return View("Error", ex.Message);
                }
            }
            return Content($"Did not find student {studentId} or course {courseId}");
        }

        private bool IsCourseAvailable(ICollection<CourseModel> studentCourseList, CourseModel course)
        {
            bool hasBought = false;
            foreach (var c in studentCourseList)
            {
                if (c.CourseId == course.CourseId)
                {
                    hasBought = true;
                    break;
                }
            }

            return hasBought;
        }
    }
}