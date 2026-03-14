using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatSticker.ViewModels
{
    public class EditChatStickerBaseModel
    {
        public ICollection<EditChatStickerViewModel> EditChatStickerViewModels { get; set; } =
            new HashSet<EditChatStickerViewModel>();

        public EditChatStickerInputModel EditChatStickerInputModel { get; set; } =
            new EditChatStickerInputModel();

        public ICollection<EditStickerStickerTypeViewModel> AllStikersTypes { get; set; } =
            new HashSet<EditStickerStickerTypeViewModel>();
    }
}