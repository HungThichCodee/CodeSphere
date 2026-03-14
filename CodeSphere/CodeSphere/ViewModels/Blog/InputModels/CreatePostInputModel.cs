using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace CodeSphere.ViewModels.Blog.InputModels
{
    public class CreatePostInputModel
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string? Content { get; set; }

        public string SanitizeContent => new HtmlSanitizer().Sanitize(this.Content);

        [Required]
        [Display(Name = "Cover Image")]
        public IFormFile? CoverImage { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Tags")]
        public ICollection<string> TagsNames { get; set; } = new HashSet<string>();

        [Display(Name = "Post Images")]
        public ICollection<IFormFile> PostImages { get; set; } = new HashSet<IFormFile>();
    }
}
