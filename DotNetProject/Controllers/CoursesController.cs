using DotNetProject.DataFolder;
using DotNetProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var courses = await _context.Courses.ToListAsync();
            return View(courses);

        }

        public async Task<IActionResult> Details(int id)
        {

            var courses = await _context.Courses.FindAsync(id);
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
            var CourseCodeExists = await _context.Courses
                         .AnyAsync(s => s.CourseCode == course.CourseCode && s.CourseId != course.CourseId);

            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(course);
            }
            if (ModelState.IsValid)
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);


        }


        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
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
            var CourseCodeExists = await _context.Courses
                        .AnyAsync(s => s.CourseCode == model.CourseCode && s.CourseId != model.CourseId);

            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(model);
            }
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //var DuplicateEnrollment = await _context.Enrollments
        //        .AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);

        //    if (DuplicateEnrollment)
        //    {
        //        ModelState.AddModelError("", "This student is already enrolled in the selected course");
        //    }


        public async Task<IActionResult> Delete(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null) return NotFound();
            return View(courses);
        }



        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
