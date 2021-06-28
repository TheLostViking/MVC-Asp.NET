using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.ViewModels.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            try
            {
                var result = await _unitOfWork.StudentRepository.GetStudentsAsync();
                var students = new List<StudentViewModel>();
                if (result == null) return NotFound();

                foreach (var s in result)
                {
                    students.Add(CreateStudentViewModel(s));
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.StudentRepository.GetStudentByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(CreateStudentViewModel(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("find/{email}")]
        public async Task<IActionResult> GetStudentByEmailAsync(string email)
        {
            try
            {
                var result = await _unitOfWork.StudentRepository.GetStudentByEmailAsync(email);
                if (result == null) return NotFound();
                return Ok(CreateStudentViewModel(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddStudent(AddStudentViewModel student)
        {
            var checkEmail = await _unitOfWork.StudentRepository.GetStudentByEmailAsync(student.Email);
            if (checkEmail != null)
            {
                throw new Exception("This Email is already registered!");
            }

            try
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    Adress = student.Adress
                };

                await _unitOfWork.StudentRepository.AddStudent(newStudent);
                if (await _unitOfWork.Complete()) return StatusCode(201);
                return StatusCode(500, "Something went wrong");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudent(int id, UpdateStudentViewModel studentModel)
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetStudentByIdAsync(id);
                if (student == null) return NotFound();

                student.FirstName = studentModel.FirstName;
                student.LastName = studentModel.LastName;
                student.Email = studentModel.Email;
                student.Adress = studentModel.Adress;
                student.Phone = studentModel.Phone;

                _unitOfWork.StudentRepository.Update(student);
                var result = await _unitOfWork.Complete();
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
                if (student == null) return NotFound();
                _unitOfWork.StudentRepository.Delete(student);
                var result = _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private StudentViewModel CreateStudentViewModel(Student student)
        {
            var model = new StudentViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                Adress = student.Adress
            };
            return model;
        }
    }
}