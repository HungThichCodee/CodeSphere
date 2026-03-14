using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.User;

namespace CodeSphere.Models.Blog
{
    public class PendingPost
    {
        [Required]
        [ForeignKey(nameof(Post))]
        public string? PostId { get; set; }

        public Post? Post { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public bool IsPending { get; set; }
    }
}
