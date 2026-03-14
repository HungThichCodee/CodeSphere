using CodeSphere.Areas.Administration.ViewModels.AddChatStickers.InputModels;
using CodeSphere.Areas.Administration.ViewModels.AddChatStickers.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.AddChatStickers
{
    public interface IAddChatStickersRepository
    {
        ICollection<AddChatStickersViewModel> GetAllStickersTypes();

        Task<Tuple<bool, string>> AddChatStickers(AddChatStickersInputModel model);
    }
}
