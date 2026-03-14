using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.ViewModels
{
    public class DeleteChatStickerBaseModel
    {
        public ICollection<DeleteChatStickerViewModel> DeleteChatStickerViewModel { get; set; } =
            new HashSet<DeleteChatStickerViewModel>();

        public DeleteChatStickerInputModel DeleteChatStickerInputModel { get; set; } =
            new DeleteChatStickerInputModel();
    }
}