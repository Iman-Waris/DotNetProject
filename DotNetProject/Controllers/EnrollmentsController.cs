using DotNetProject.DataFolder;
using DotNetProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments
                .ToListAsync();
            return View(enrollments);
        }

        public async Task<IActionResult> Create()
        {

            var students = await _context.Students.ToListAsync();
            var courses = await _context.Courses.ToListAsync();
            ViewBag.StudentList = new SelectList(students, "StudentId", "FirstName");
            ViewBag.CourseList = new SelectList(courses, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enrollments model)

        {
            var DuplicateEnrollment = await _context.Enrollments
                    .AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }

            if (ModelState.IsValid)
            {
                ViewBag.StudentList = new SelectList(await _context.Students.ToListAsync(), "StudentId", "FirstName", model.StudentId);
                ViewBag.CourseList = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", model.CourseId);
                return View(model);
            }
            _context.Enrollments.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)

        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            var students = await _context.Students.ToListAsync();
            var courses = await _context.Courses.ToListAsync();
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
            var DuplicateEnrollment = await _context.Enrollments
                      .AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
