using System.ComponentModel.DataAnnotations;

namespace DotNetProject.Entities
{
    public class Enrollments
    {
        [Key]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string? Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public Students Student { get; set; }
        public Courses Course { get; set; }


    }
}
