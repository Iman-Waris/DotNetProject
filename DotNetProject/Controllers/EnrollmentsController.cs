using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using DotNetProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetProject.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentsController(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }



        public async Task<IActionResult> Index()
        {
            var enrollments = await _unitOfWork.EnrollmentRepository.ToListAsync();

            return View(enrollments);
        }

        public async Task<IActionResult> Create()
        {

            var students = await _unitOfWork.StudentRepository.ToListAsync();
            var courses = await _unitOfWork.CourseRepository.ToListAsync();
            ViewBag.StudentList = new SelectList(students, "StudentId", "FirstName");
            ViewBag.CourseList = new SelectList(courses, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enrollments model)

        {
            var DuplicateEnrollment = await _unitOfWork.EnrollmentRepository
                    .AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }

            if (ModelState.IsValid)
            {
                ViewBag.StudentList = new SelectList(await _unitOfWork.StudentRepository.ToListAsync(), "StudentId", "FirstName", model.StudentId);
                ViewBag.CourseList = new SelectList(await _unitOfWork.CourseRepository.ToListAsync(), "CourseId", "CourseName", model.CourseId);
                return View(model);
            }
            await _unitOfWork.EnrollmentRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)

        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            var students = await _unitOfWork.StudentRepository.ToListAsync();
            var courses = await _unitOfWork.CourseRepository.ToListAsync();
            ViewBag.StudentList = new SelectList(students, "StudentId", "FirstName", enrollment.StudentId);
            ViewBag.CourseList = new SelectList(courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Enrollments model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var DuplicateEnrollment = await _unitOfWork.EnrollmentRepository
                      .AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }

            _unitOfWork.EnrollmentRepository.Update(model);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            _unitOfWork.EnrollmentRepository.Delete(enrollment);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
