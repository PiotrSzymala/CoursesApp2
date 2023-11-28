using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoursesApp.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nickname { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }

        public ICollection<Signup> Signups { get; set; }

        public ICollection<ForumPost> ForumPosts { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
