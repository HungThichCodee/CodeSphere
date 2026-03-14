using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.AddEmoji.InputModels
{
    public class AddEmojiInputModel
    {
        [Required]
        [MaxLength(120)]
        [Display(Name = "Emoji Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Emoji Image")]
        public IFormFile Image { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        [EnumDataType(typeof(EmojiType))]
        [Display(Name = "Emoji Type")]
        public EmojiType EmojiType { get; set; }
    }
}