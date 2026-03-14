using CodeSphere.Areas.PrivateChat.ViewModels.CollectStickers.ViewModels;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.Repositories.CollectStickers
{
    public interface ICollectStickersRepository
    {
        ICollection<CollectStickersStickerTypeViewModel> GetAllStickers(ApplicationUser currentUser);

        Task<bool> AddStickerToFavourite(ApplicationUser currentUser, string stickerTypeId);

        Task<bool> RemoveStickerFromFavourite(ApplicationUser currentUser, string stickerTypeId);
    }
}
