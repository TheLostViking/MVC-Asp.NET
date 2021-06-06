using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _repo;

        public CoursesController(IUnitOfWork unitOfWork, ICourseRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _repo.GetCoursesAsync();
            return Ok(result);
        }

        [HttpGet("{courseNumber}")]
        public async Task<IActionResult> GetCourses(int courseNumber)
        {
            var result = await _repo.GetCoursesByCourseNoAsync(courseNumber);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(Course course)
        {
            try
            {
                await _repo.Add(course);
                if (await _unitOfWork.Complete()) return StatusCode(201);
                return StatusCode(500, "Something went wrong!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course courseModel)
        {
            try
            {
                var course = await _repo.GetCoursesByIdAsync(id);
                course.Title = courseModel.Title;
                course.Description = courseModel.Description;
                course.CourseNumber = courseModel.CourseNumber;
                course.Length = courseModel.Length;
                course.Level = courseModel.Level;
                course.Active = courseModel.Active;
                course.Price = courseModel.Price;

                _repo.Update(course);
                var result = await _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _repo.GetCoursesByIdAsync(id);
                if (course == null) return NotFound();

                _repo.Delete(course);
                var result = _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}