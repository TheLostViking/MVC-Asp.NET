using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var result = await _unitOfWork.CourseRepository.GetCoursesAsync();
                var courses = new List<CourseViewModel>();
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
            if(checkCourseNo != null)
            {
                throw new Exception("There is already a course with this course number!");
            }

            try
            {
                var newCourse = new Course 
                {
                    CourseNumber = course.CourseNumber,
                    Title = course.Title,
                    Description = course.Description,
                    Length = course.Length,
                    Level = course.Level,
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
            if (checkCourseNo != null && checkCourseNo.Id != courseModel.Id)
            {
                throw new Exception ("There's already a course with this course number!");
            }
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                if (course == null) return NotFound();

                course.Title = courseModel.Title;
                course.Description = courseModel.Description;
                course.CourseNumber = courseModel.CourseNumber;
                course.Length = courseModel.Length;
                course.Level = courseModel.Level;
                course.Price = courseModel.Price;

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

        private CourseViewModel CreateCourseViewModel(Course course)
        {
            var model = new CourseViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                CourseNumber = course.CourseNumber,
                Description = course.Description,
                Length = course.Length,
                Level = course.Level,
                Price = course.Price,
                Active = course.Active
            };
            return model;
        }
    }
}