using CodeSphere.Areas.Administration.ViewModels.AddChatStickers.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.AddChatStickers.ViewModels
{

    public class AddChatStickersBaseModel
    {
        public ICollection<AddChatStickersViewModel> AddChatStickersViewModels { get; set; } =
            new HashSet<AddChatStickersViewModel>();

        public AddChatStickersInputModel AddChatStickersInputModel { get; set; } =
            new AddChatStickersInputModel();
    }
}