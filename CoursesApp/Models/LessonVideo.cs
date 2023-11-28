using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesApp.Models
{
    public class LessonVideo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public bool Active { get; set; }

        [ForeignKey("Lesson")] 
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
