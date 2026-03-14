using CodeSphere.Areas.Administration.ViewModels.AllChatStickers.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.AllChatStickers
{
    public interface IAllChatStickersRepository
    {
        IEnumerable<AllChatStickersViewModel> GetAllChatStickers();
    }
}
