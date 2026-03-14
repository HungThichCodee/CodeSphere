using System.ComponentModel.DataAnnotations;
using CodeSphere.Constraints;
using Microsoft.Extensions.Hosting;

namespace CodeSphere.Models.Blog
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.BlogCategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
