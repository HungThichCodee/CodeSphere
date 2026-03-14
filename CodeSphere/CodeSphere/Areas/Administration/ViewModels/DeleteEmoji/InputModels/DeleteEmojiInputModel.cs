using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.InputModels
{
    public class DeleteEmojiInputModel
    {
        [Required]
        [Display(Name = "Emoji Name")]
        public string Id { get; set; }
    }
}