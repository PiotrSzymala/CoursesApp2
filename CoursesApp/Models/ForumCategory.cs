using System.ComponentModel.DataAnnotations;

namespace CoursesApp.Models
{
    public class ForumCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<ForumPost> ForumPosts { get; set; }
    }
}
