using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.EditEmoji.InputModels
{
    public class EditEmojiInputModel
    {
        [Required]
        [Display(Name = "Emoji Name")]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Emoji Name")]
        public string Name { get; set; }

        [Display(Name = "Emoji Image")]
        public IFormFile Image { get; set; }

        [Required]
        [EnumDataType(typeof(EmojiType))]
        [Display(Name = "Emoji Type")]
        public EmojiType EmojiType { get; set; }
    }
}