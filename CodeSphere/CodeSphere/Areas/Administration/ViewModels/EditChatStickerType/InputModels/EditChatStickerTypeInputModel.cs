using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.InputModels
{
    public class EditChatStickerTypeInputModel
    {
        [Display(Name = "Sticker Type Name")]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Sticker Type Name")]
        public string Name { get; set; }

        [Display(Name = "Sticker Type Image")]
        public IFormFile Image { get; set; }
    }
}