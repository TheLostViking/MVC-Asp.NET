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

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, object model)
        {
            return NoContent();
        }
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            return NoContent();
        }
    }
}