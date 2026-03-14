using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.AddEmojis.InputModels
{
    public class AddEmojisInputModel
    {
        [Required]
        [Display(Name = "Emojis Type")]
        [EnumDataType(typeof(EmojiType))]
        public EmojiType EmojiType { get; set; }

        [Required]
        [Display(Name = "Emojis Images")]
        public ICollection<IFormFile> Images { get; set; } = new HashSet<IFormFile>();
    }
}