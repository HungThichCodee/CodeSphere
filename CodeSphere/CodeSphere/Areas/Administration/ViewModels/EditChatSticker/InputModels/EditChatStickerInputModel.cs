using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatSticker.InputModels
{
    public class EditChatStickerInputModel
    {
        [Required]
        [Display(Name = "Sticker Name")]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Sticker Name")]
        public string Name { get; set; }

        [Display(Name = "Sticker Image")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Sticker Type Name")]
        public string StickerTypeId { get; set; }
    }
}