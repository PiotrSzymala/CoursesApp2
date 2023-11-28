using System.ComponentModel.DataAnnotations;

namespace CoursesApp.Models
{
    public class SignupMessage
    {
        [Key]
        public int SignupMessageId { get; set; }

        [Required]
        public string Message { get; set; }

        public Course Course { get; set; }
    }
}
