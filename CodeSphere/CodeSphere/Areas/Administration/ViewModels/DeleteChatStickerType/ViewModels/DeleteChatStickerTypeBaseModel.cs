using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.ViewModels
{
    public class DeleteChatStickerTypeBaseModel
    {
        public ICollection<DeleteChatStickerTypeViewModel> DeleteChatStickerTypeViewModel { get; set; } =
            new HashSet<DeleteChatStickerTypeViewModel>();

        public DeleteChatStickerTypeInputModel DeleteChatStickerTypeInputModel { get; set; } =
            new DeleteChatStickerTypeInputModel();
    }
}