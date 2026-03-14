using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatSticker.InputModels
{
    public class AddChatStickerInputModel
    {
        [Required]
        [MaxLength(120)]
        [Display(Name = "Sticker Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sticker Image")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Sticker Type")]
        public string StickerTypeId { get; set; }
    }
}
