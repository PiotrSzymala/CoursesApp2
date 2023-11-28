using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesApp.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string PriceString { get; set; }

        [Required]
        public bool HasHomework { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public LessonVideo LessonVideo { get; set; }
    }
}
