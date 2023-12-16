using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesApp.Models
{
    public class ForumPost
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } // Zmiana na string, aby pasowało do klucza głównego User

        public User User { get; set; }

        [Required]
        [ForeignKey("ForumCategory")]
        public int CategoryId { get; set; }
        public ForumCategory ForumCategory { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AttachedMedia { get; set; }
    }
}
