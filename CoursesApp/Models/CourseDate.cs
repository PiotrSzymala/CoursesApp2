using System.ComponentModel.DataAnnotations;

namespace CoursesApp.Models
{
    public class CourseDate
    {
        [Key]
        public int CourseDateId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Weekday { get; set; }

        [Required]
        public int Hour { get; set; }

        public Signup Signup { get; set; }
    }
}
