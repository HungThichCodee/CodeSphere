using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Constraints;

namespace CodeSphere.Models.Blog
{
    public class PostImage
    {
        public PostImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.PostImageNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
