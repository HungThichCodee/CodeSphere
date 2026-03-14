using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.EditChatSticker
{
    public interface IEditChatStickerRepository
    {
        ICollection<EditStickerStickerTypeViewModel> GetAllStikersTypes();

        ICollection<EditChatStickerViewModel> GetAllStickers();

        Task<GetEditChatStickerDataViewModel> GetStickerById(string stickerId);

        Task<Tuple<bool, string>> EditSticker(EditChatStickerInputModel model);
    }
}