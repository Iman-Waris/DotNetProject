using DotNetProject.DataFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Controllers
{
    public class DashboardController : Controller

    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        { _context = context; }
        public async Task<IActionResult> Index()

        {
            ViewBag.TotalStudents = await _context.Students.CountAsync();
            ViewBag.TotalCourses = await _context.Courses.CountAsync();
            ViewBag.TotalEnrollments = await _context.Enrollments.CountAsync();
            ViewBag.AvgStudentsPerCourse =
            (ViewBag.TotalCourses == 0 ? 0 :
            (double)ViewBag.TotalEnrollments / ViewBag.TotalCourses);

            // ///////////////////////////////5)

            ViewBag.TopStudents = await _context.Enrollments
            .GroupBy(e => e.StudentId)
            .Select(g => new { StudentId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Join(_context.Students,
            g => g.StudentId,
            s => s.StudentId,
                  (g, s) => new
                  {
                      s.StudentId,
                      Name = s.FirstName + " " + s.LastName,
                      g.Count
                  })
               .ToListAsync();
            /////////////////////////////


            DateOnly fromDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
            ViewBag.RecentStudents = await _context.Students
                .Where(s => s.EnrollmentDate >= fromDate)
                .OrderByDescending(s => s.EnrollmentDate)
                .Take(5)
                .ToListAsync();


            ///////////

            ViewBag.PopularCourses = await _context.Enrollments
        .GroupBy(e => e.CourseId)
        .Select(g => new
        {
            CourseId = g.Key,
            EnrollmentCount = g.Count()
        })
        .Join(_context.Courses, g => g.CourseId, c => c.CourseId, (g, c) => new
        {
            c.CourseId,
            c.CourseName,
            g.EnrollmentCount
        })
        .OrderByDescending(x => x.EnrollmentCount)
        .Take(5)
        .ToListAsync();




            ////////////////////////////


            ViewBag.CourseAvgGrades = await _context.Enrollments
         .Where(e => e.Grade != null)
       .GroupBy(e => e.CourseId)
          .Select(g => new
          {
              CourseId = g.Key,
              AvgGrade = g.Average(e =>
                  e.Grade == "A" ? 4 :
                  e.Grade == "B" ? 3 :
                  e.Grade == "C" ? 2 :
                  e.Grade == "D" ? 1 : 0)
          })
     .OrderByDescending(x => x.AvgGrade)
     .ToListAsync();

            //////////////////////

            ViewBag.GradeDistribution = await _context.Enrollments
                .Where(e => e.Grade != null)
                .GroupBy(e => e.Grade)
                .Select(g => new
                {
                    Grade = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();


            ///////////////////


            return View();

        }

    }
}