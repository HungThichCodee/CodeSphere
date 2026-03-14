using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Areas.PrivateChat.ViewModels.CollectStickers.ViewModels;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.PrivateChat.Repositories.CollectStickers
{
    public class CollectStickersRepository : ICollectStickersRepository
    {
        private readonly ApplicationDbContext db;

        public CollectStickersRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddStickerToFavourite(ApplicationUser currentUser, string stickerTypeId)
        {
            var targetStickerType = await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == stickerTypeId);

            if (targetStickerType != null)
            {
                var targetFavourite = await this.db.FavouriteStickers.FirstOrDefaultAsync(x => x.ApplicationUserId == currentUser.Id &&
                    x.StickerTypeId == stickerTypeId);

                if (targetFavourite != null)
                {
                    targetFavourite.IsFavourite = true;
                    this.db.FavouriteStickers.Update(targetFavourite);
                    await this.db.SaveChangesAsync();
                    return true;
                }

                this.db.FavouriteStickers.Add(new FavouriteStickers
                {
                    ApplicationUserId = currentUser.Id,
                    StickerTypeId = targetStickerType.Id,
                    IsFavourite = true,
                });

                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public ICollection<CollectStickersStickerTypeViewModel> GetAllStickers(ApplicationUser currentUser)
        {
            var result = new List<CollectStickersStickerTypeViewModel>();

            var allStickerTypes = this.db.StickerTypes.OrderBy(x => x.Name).ThenBy(x => x.Position).ToList();

            foreach (var stickerType in allStickerTypes)
            {
                var targetStickerType = new CollectStickersStickerTypeViewModel
                {
                    Id = stickerType.Id,
                    Name = stickerType.Name,
                    Url = stickerType.Url,
                };

                if (this.db.FavouriteStickers
                    .Any(x => x.StickerTypeId == stickerType.Id &&
                        x.IsFavourite &&
                        x.ApplicationUserId == currentUser.Id))
                {
                    var haveIt = this.db.FavouriteStickers
                        .FirstOrDefault(x => x.StickerTypeId == stickerType.Id &&
                            x.IsFavourite &&
                            x.ApplicationUserId == currentUser.Id)
                        .IsFavourite;
                    targetStickerType.HaveIt = haveIt;
                }
                else
                {
                    targetStickerType.HaveIt = false;
                }

                var allStickers = this.db.Stickers
                    .Where(x => x.StickerTypeId == stickerType.Id)
                    .OrderBy(x => x.Position)
                    .ToList();

                foreach (var sticker in allStickers)
                {
                    targetStickerType.AllStickers.Add(new CollectStickersStickerViewModel
                    {
                        Id = sticker.Id,
                        Name = sticker.Name,
                        Url = sticker.Url,
                    });
                }

                result.Add(targetStickerType);
            }

            return result;
        }

        public async Task<bool> RemoveStickerFromFavourite(ApplicationUser currentUser, string stickerTypeId)
        {
            var targetConnection = await this.db.FavouriteStickers
                .FirstOrDefaultAsync(x => x.ApplicationUserId == currentUser.Id &&
                    x.StickerTypeId == stickerTypeId &&
                    x.IsFavourite == true);

            if (targetConnection != null)
            {
                targetConnection.IsFavourite = false;
                this.db.FavouriteStickers.Update(targetConnection);
                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
