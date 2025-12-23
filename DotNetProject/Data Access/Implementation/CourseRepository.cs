using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using DotNetProject.Entities;

namespace DotNetProject.Data_Access.Implementation
{
    public class CourseRepository : Repository<Courses, int>, ICourseRepository
    {



        public CourseRepository(AppDbContext context) : base(context)
        {



        }

        private AppDbContext AppDbContext => AppDbContext as AppDbContext;





    }
}
