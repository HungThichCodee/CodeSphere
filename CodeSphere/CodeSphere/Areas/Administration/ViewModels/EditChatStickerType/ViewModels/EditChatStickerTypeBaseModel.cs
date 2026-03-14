using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.ViewModels
{
    public class EditChatStickerTypeBaseModel
    {
        public ICollection<EditChatStickerTypeViewModel> EditChatStickerTypeViewModels { get; set; } =
            new HashSet<EditChatStickerTypeViewModel>();

        public EditChatStickerTypeInputModel EditChatStickerTypeInputModel { get; set; } =
            new EditChatStickerTypeInputModel();
    }
}