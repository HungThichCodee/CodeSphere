using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatTheme.InputModels
{
    public class AddChatThemeInputModel
    {
        [Required]
        [MaxLength(30)]
        [Display(Name = "Theme Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Theme Image")]
        public IFormFile Image { get; set; }
    }
}