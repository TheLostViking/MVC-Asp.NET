using System.Threading.Tasks;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentService _service;

        public StudentsController(IUnitOfWork unitOfWork, IStudentService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetStudentsAsync();
            return View("Index", result);
        }
    }
}