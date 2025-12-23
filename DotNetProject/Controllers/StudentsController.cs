using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using DotNetProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Controllers
{
    public class StudentsController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var students = await _unitOfWork.StudentRepository.ToListAsync();
        //    return View(students);
        //}

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);

        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Students student)
        {
            var EmailExists = await _unitOfWork.StudentRepository
                        .AnyAsync(s => s.Email == student.Email && s.StudentId != student.StudentId);

            if (EmailExists)
            {
                ModelState.AddModelError("Email", "Email   already in use");
                return View(student);
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.StudentRepository.AddAsync(student);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Students model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var EmailExists = await _unitOfWork.StudentRepository
                            .AnyAsync(s => s.Email == model.Email && s.StudentId != model.StudentId);

            if (EmailExists)
            {
                ModelState.AddModelError("Email", "Email   already in use");
                return View(model);
            }
            _context.Update(model);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var students = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (students == null) return NotFound();
            return View(students);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _unitOfWork.StudentRepository.Delete(student);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
