using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _context.Students.ToListAsync();
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddStudent(Student student)
        {
              try
            {
                _context.Students.Add(student);
                var result = await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}