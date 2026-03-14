using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatSticker
{
    public interface IDeleteChatStickerRepository
    {
        ICollection<DeleteChatStickerViewModel> GetAllStickers();

        Task<string> GetStickerUrl(string stickerId);

        Task<Tuple<bool, string>> DeleteChatSticker(DeleteChatStickerInputModel model);
    }
}
