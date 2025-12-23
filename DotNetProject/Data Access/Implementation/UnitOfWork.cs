using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;

namespace DotNetProject.Data_Access.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public UnitOfWork(AppDbContext context, ICourseRepository courseRepo, IStudentRepository studentRepo, IEnrollmentRepository enrollmentRepo)
        {
            _context = context;
            _courseRepository = courseRepo;
            _studentRepository = studentRepo;
            _enrollmentRepository = enrollmentRepo;
        }


        public ICourseRepository CourseRepository => _courseRepository ?? new CourseRepository(_context);
        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(_context);
        public IEnrollmentRepository EnrollmentRepository => _enrollmentRepository ?? new EnrollmentRepository(_context);



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //todo
        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
