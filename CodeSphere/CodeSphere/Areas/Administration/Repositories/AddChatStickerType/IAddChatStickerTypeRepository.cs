using CodeSphere.Areas.Administration.ViewModels.AddChatStickerType.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddChatStickerType
{
    public interface IAddChatStickerTypeRepository
    {
        Task<Tuple<bool, string>> AddNewStickerType(AddChatStickerTypeInputModel model);
    }
}
