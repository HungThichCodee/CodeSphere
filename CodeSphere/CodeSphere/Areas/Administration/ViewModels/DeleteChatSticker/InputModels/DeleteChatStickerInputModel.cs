using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.InputModels
{
    public class DeleteChatStickerInputModel
    {
        [Required]
        [Display(Name = "Sticker Name")]
        public string Id { get; set; }
    }
}