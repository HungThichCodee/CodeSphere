using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatStickerType
{
    public interface IDeleteChatStickerTypeRepository
    {
        ICollection<DeleteChatStickerTypeViewModel> GetAllStickersTypes();

        List<string> GetStickersUrls(string stickerTypeId);

        Task<Tuple<bool, string>> DeleteChatStickerType(DeleteChatStickerTypeInputModel model);
    }
}
