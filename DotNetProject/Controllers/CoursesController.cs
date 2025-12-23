using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using DotNetProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {

            var courses = await _unitOfWork.CourseRepository.ToListAsync();
            return View(courses);

        }

        public async Task<IActionResult> Details(int id)
        {

            var courses = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Courses course)

        {

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            var CourseCodeExists = await _unitOfWork.CourseRepository
                         .AnyAsync(s => s.CourseCode == course.CourseCode && s.CourseId != course.CourseId);

            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(course);
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.CourseRepository.AddAsync(course);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);


        }


        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Courses model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var CourseCodeExists = await _unitOfWork.CourseRepository
                        .AnyAsync(s => s.CourseCode == model.CourseCode && s.CourseId != model.CourseId);

            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(model);
            }
            _unitOfWork.CourseRepository.Update(model);
            await _unitOfWork.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var courses = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (courses == null) return NotFound();
            return View(courses);
        }



        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _unitOfWork.CourseRepository.Delete(course);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
