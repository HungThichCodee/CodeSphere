using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.InputModels
{
    public class DeleteChatStickerTypeInputModel
    {
        [Required]
        [Display(Name = "Sticker Type Name")]
        public string Id { get; set; }
    }
}