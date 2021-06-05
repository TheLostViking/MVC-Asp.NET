using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _context.Courses.ToListAsync();
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                var result = await _context.SaveChangesAsync();
                return StatusCode(201);
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
                var course = await _context.Courses.FindAsync(id);
                course.Title = courseModel.Title;
                course.Description = courseModel.Description;
                course.Category = courseModel.Category;
                course.Length = courseModel.Length;
                course.Price = courseModel.Price;

                _context.Update(course);
                var result = await _context.SaveChangesAsync();
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
                var course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == id);
                if (course == null) return NotFound();
                _context.Courses.Remove(course);
                var result = _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}