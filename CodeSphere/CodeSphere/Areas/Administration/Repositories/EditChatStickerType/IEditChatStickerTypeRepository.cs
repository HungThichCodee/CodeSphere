using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.EditChatStickerType
{
    public interface IEditChatStickerTypeRepository
    {
        ICollection<EditChatStickerTypeViewModel> GetAllChatStickerTypes();

        Task<GetEditChatStickerTypeDataViewModel> GetStickerTypeById(string stickerTypeId);

        Task<Tuple<bool, string>> EditStickerType(EditChatStickerTypeInputModel model);
    }
}