using Api.Data;
using Api.Entities;
using Api.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _unitOfWork.StudentRepository.GetStudentsAsync();
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddStudent(Student student)
        {
              try
            {
                await _unitOfWork.StudentRepository.AddStudent(student);
                var result = await _unitOfWork.Complete();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudent(int id, Student studentModel)
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetStudentByIdAsync(id);
                student.FirstName = studentModel.FirstName;
                student.LastName = studentModel.LastName;
                student.Adress = studentModel.Adress;
                student.Phone = studentModel.Phone;
                _unitOfWork.StudentRepository.Update(student);
                var result = _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetStudentByIdAsync(id);
                if(student == null) return NotFound();
                _unitOfWork.StudentRepository.Delete(student);
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