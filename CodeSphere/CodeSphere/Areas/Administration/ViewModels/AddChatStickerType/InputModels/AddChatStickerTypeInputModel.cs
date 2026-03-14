using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatStickerType.InputModels
{

    public class AddChatStickerTypeInputModel
    {
        [Required]
        [MaxLength(120)]
        [Display(Name = "Sticker Type Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sticker Type Image")]
        public IFormFile Image { get; set; }
    }
}