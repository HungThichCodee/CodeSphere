using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatTheme.InputModels
{
    public class EditChatThemeInputModel
    {
        [Required]
        [Display(Name = "Theme Name")]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Theme Name")]
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}