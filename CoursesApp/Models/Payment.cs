using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}