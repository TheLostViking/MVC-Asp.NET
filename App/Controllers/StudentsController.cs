using System;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetStudentsAsync();
            return View("Index", result);
        }

        [HttpGet()]
        public IActionResult AddStudent()
        {
            return View("AddStudent");
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("AddStudent", data);
            }

            var student = new StudentModel
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Phone = data.Phone,
                Adress = data.Adress
            };

            try
            {
                if (await _service.AddStudent(student))
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

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _service.GetStudentByIdAsync(id);
            if(await _service.DeleteStudent(student.Id)) return RedirectToAction("Index");
            return View("Error");
        }
        [HttpGet()]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _service.GetStudentByIdAsync(id);

            var model = new EditStudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                Adress = student.Adress
            };

            return View("EditStudent", model);
        }

        [HttpPost()]
        public async Task<IActionResult> EditStudent(EditStudentViewModel data)
        {
            if(!ModelState.IsValid)
            {
                return View("EditStudent", data);
            }

            var student = await _service.GetStudentByIdAsync(data.Id);

            student.FirstName = data.FirstName;
            student.LastName = data.LastName;
            student.Email = data.Email;
            student.Phone = data.Phone;
            student.Adress = data.Adress;

            try
            {
                if(await _service.EditStudent(student.Id, student)) return RedirectToAction("Index");                
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Error");
        }
    }
}