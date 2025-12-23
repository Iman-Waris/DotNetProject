using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetProject.Entities
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length is strict")]
        public string CourseName { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [DisplayName("Credits")]
        [Range(2, 100, ErrorMessage = "Strict")]
        public int Credits { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();



    }
}
