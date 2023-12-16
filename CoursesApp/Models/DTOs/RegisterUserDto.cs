using System.ComponentModel.DataAnnotations;

namespace CoursesApp.Models.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
