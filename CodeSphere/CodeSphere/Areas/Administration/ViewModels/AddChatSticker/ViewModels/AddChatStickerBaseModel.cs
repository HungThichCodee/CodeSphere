using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatSticker.ViewModels
{
    public class AddChatStickerBaseModel
    {
        public ICollection<AddChatStickerViewModel> AddChatStickerViewModels { get; set; } =
            new HashSet<AddChatStickerViewModel>();

        public AddChatStickerInputModel AddChatStickerInputModel { get; set; } = new AddChatStickerInputModel();
    }
}
