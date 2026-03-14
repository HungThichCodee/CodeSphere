using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Models.Blog
{
    public class PostTag
    {
        [Required]
        [ForeignKey(nameof(Post))]
        public string? PostId { get; set; }

        public Post? Post { get; set; }

        [Required]
        [ForeignKey(nameof(Tag))]
        public string? TagId { get; set; }

        public Tag? Tag { get; set; }
    }
}
