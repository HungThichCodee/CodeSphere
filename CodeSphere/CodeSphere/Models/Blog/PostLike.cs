using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.User;

namespace CodeSphere.Models.Blog
{
    public class PostLike
    {
        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public string? PostId { get; set; }

        public Post? Post { get; set; }

        [Required]
        public bool IsLiked { get; set; }
    }
}
