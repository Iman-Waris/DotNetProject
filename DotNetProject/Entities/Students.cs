using System.ComponentModel.DataAnnotations;
namespace DotNetProject.Entities


{
    public class Students
    {

        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public DateOnly EnrollmentDate { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();


    }
}
