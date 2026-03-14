using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.AddChatSticker
{
    public interface IAddChatStickerRepository
    {
        ICollection<AddChatStickerViewModel> GetAllStickerTypes();

        Task<Tuple<bool, string>> AddNewSticker(AddChatStickerInputModel model);
    }
}
