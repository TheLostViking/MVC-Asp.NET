using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Interfaces;
using Api.ViewModels;
using Api.ViewModels.Students;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            // try
            // {
            //     var courses = new List<Course>();
            //     var result = await _unitOfWork.CourseRepository.GetCoursesAsync();

            //     if (result == null) return NotFound();
            //     foreach (var c in result)
            //     {
            //         courses.Add(c);
            //     }
            //     return Ok(result);
            // }
            try
            {
                var courses = new List<CourseViewModel>();
                var result = await _unitOfWork.CourseRepository.GetCoursesAsync();                

                if (result == null) return NotFound();

                foreach (var c in result)
                {
                    courses.Add(CreateCourseViewModel(c));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetActiveCourses(string status)
        {
            try
            {
                var activeCourses = new List<CourseViewModel>();
                var result = await _unitOfWork.CourseRepository.GetCoursesByStatusAsync(status);

                if (result == null) return NotFound();

                foreach (var c in result)
                {
                    if (c.Status.Name == "Active")
                    {
                        activeCourses.Add(CreateCourseViewModel(c));
                    }
                }
                return Ok(activeCourses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var result = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(CreateCourseViewModel(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find/{courseNumber}")]
        public async Task<IActionResult> GetCourseByCourseNumber(int courseNumber)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseNumber);
                if (course == null) return NotFound();
                return Ok(CreateCourseViewModel(course));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(AddCourseViewModel course)
        {
            var checkCourseNo = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(course.CourseNumber);
            if (checkCourseNo != null)
            {
                throw new Exception("There is already a course with this course number!");
            }

            // var checkLevel = await _unitOfWork.LevelRepository.GetLevelsAsync();
            // var level = new Level();
            // foreach (var l in checkLevel)
            // {
            //     if(l.Id == course.Level)
            //     {
            //         level = l;
            //     }
            // }

            try
            {
                var level = await _unitOfWork.LevelRepository.GetLevelAsync(course.Level);
                if (level == null) return NotFound("Could not find level: " + course.Level);
                var newCourse = new Course
                {
                    CourseNumber = course.CourseNumber,
                    Title = course.Title,
                    Description = course.Description,
                    Length = course.Length,
                    LevelId = level.Id,
                    Price = course.Price
                };

                await _unitOfWork.CourseRepository.Add(newCourse);

                if (await _unitOfWork.Complete()) return StatusCode(201);

                return StatusCode(500, "Something went wrong!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseViewModel courseModel)
        {

            var checkCourseNo = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseModel.CourseNumber);
            if (checkCourseNo != null && checkCourseNo.CourseId != courseModel.CourseId)
            {
                throw new Exception("There's already a course with this course number!");
            }
            try
            {
                var level = await _unitOfWork.LevelRepository.GetLevelAsync(courseModel.Level);
                if (level == null) return NotFound("Could not find level: " + courseModel.Level);

                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                if (course == null) return NotFound();

                course.Title = courseModel.Title;
                course.Description = courseModel.Description;
                course.CourseNumber = courseModel.CourseNumber;
                course.Length = courseModel.Length;
                course.Price = courseModel.Price;
                course.LevelId = level.Id;

                _unitOfWork.CourseRepository.Update(course);
                var result = await _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{courseNumber}")]
        public async Task<IActionResult> DeleteCourse(int courseNumber)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseNumber);
                if (course == null) return NotFound();

                _unitOfWork.CourseRepository.Delete(course);
                var result = _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("levels")]
        public async Task<IActionResult> GetLevels()
        {
            try
            {
                var result = await _unitOfWork.LevelRepository.GetLevelsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SetCourseInactiveAsync(int id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                if (course == null) return NotFound();

                _unitOfWork.CourseRepository.SetInavticeAsync(course.CourseId);
                var result = await _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private CourseViewModel CreateCourseViewModel(Course course)
        {
            var model = new CourseViewModel()
            {
                CourseId = course.CourseId,
                Title = course.Title,
                CourseNumber = course.CourseNumber,
                Description = course.Description,
                Length = course.Length,
                Level = course.Level.Name,
                Price = course.Price
            };
            model.Students = new List<StudentViewModel>();
            foreach (var cs in course.CourseStudents)
            {
                var sModel = new StudentViewModel()
                {
                    StudentId = cs.StudentId,
                    FirstName = cs.Student.FirstName,
                    LastName = cs.Student.LastName,
                    Email = cs.Student.Email,
                    Phone = cs.Student.Phone,
                    Adress = cs.Student.Adress
                };
                model.Students.Add(sModel);
            };
            return model;
        }
    }
}