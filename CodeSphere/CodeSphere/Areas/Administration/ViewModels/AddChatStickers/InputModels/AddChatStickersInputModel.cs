using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatStickers.InputModels
{

    public class AddChatStickersInputModel
    {
        [Required]
        [Display(Name = "Sticker Type Name")]
        public string StickerTypeId { get; set; }

        [Required]
        [Display(Name = "Stickers Images")]
        public IFormFile[] Images { get; set; }
    }
}