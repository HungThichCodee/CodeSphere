using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.InputModels
{
    public class DeleteChatThemeInputModel
    {
        [Required]
        [Display(Name = "Theme Name")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Theme Name")]
        public string Name { get; set; }
    }
}