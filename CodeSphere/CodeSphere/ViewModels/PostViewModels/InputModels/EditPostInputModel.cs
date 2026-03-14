using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace CodeSphere.ViewModels.PostViewModels.InputModels
{
    public class EditPostInputModel
    {
        public string? Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string? Content { get; set; }

        public string SanitizeContent => new HtmlSanitizer().Sanitize(this.Content);

        [Display(Name = "Cover Image")]
        public IFormFile? CoverImage { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Tags")]
        public ICollection<string> TagsNames { get; set; } = new HashSet<string>();

        public ICollection<string> Categories { get; set; } = new HashSet<string>();

        public ICollection<string> Tags { get; set; } = new HashSet<string>();
    }
}
