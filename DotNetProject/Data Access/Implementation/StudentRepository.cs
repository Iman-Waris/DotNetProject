using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using DotNetProject.Entities;

namespace DotNetProject.Data_Access.Implementation
{
    public class StudentRepository : Repository<Students, int>, IStudentRepository
    {


        public StudentRepository(AppDbContext context) : base(context)
        {


        }

        private AppDbContext AppDbContext => AppDbContext as AppDbContext;





    }
}
