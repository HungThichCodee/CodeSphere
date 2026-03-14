using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteEmojisByType.InputModels
{
    public class DeleteEmojisByTypeInputModel
    {
        [Required]
        [EnumDataType(typeof(EmojiType))]
        [Display(Name = "Emoji Type")]
        public EmojiType EmojiType { get; set; }
    }
}