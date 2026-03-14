using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace CodeSphere.Areas.Editor.ViewModels
{
    public class AddCategoryInputModel
    {
        [Required]
        [MaxLength(30)]
        [Display(Name = "Category name")]
        public string? Name { get; set; }

        [Display(Name = "Category description")]
        [Required]
        public string? Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);
    }
}
