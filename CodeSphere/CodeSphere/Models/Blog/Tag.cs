using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Constraints;

namespace CodeSphere.Models.Blog
{
    public class Tag
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.BlogPostTagNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public ICollection<PostTag> TagsPosts { get; set; } = new HashSet<PostTag>();
    }
}
