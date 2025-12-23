namespace DotNetProject.Data_Access.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        Task<int> CommitAsync();

        IStudentRepository StudentRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
    }
}
