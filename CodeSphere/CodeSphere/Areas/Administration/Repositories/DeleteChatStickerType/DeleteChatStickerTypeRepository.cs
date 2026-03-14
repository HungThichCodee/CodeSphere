using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatStickerType
{
    public class DeleteChatStickerTypeRepository : IDeleteChatStickerTypeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public DeleteChatStickerTypeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> DeleteChatStickerType(DeleteChatStickerTypeInputModel model)
        {
            var targetStickerType = await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (targetStickerType != null)
            {
                var favouriteStickerTypes = this.db.FavouriteStickers
                    .Where(x => x.StickerTypeId == targetStickerType.Id)
                    .ToList();

                string name = targetStickerType.Name;
                int count = 0;

                var allStickers = this.db.Stickers.Where(x => x.StickerTypeId == targetStickerType.Id).ToList();

                foreach (var sticker in allStickers)
                {
                    ApplicationCloudinary.DeleteImage(
                        this.cloudinary,
                        string.Format(GlobalConstants.StickerName, sticker.Id),
                        GlobalConstants.StickersFolder);
                    count++;
                }

                ApplicationCloudinary.DeleteImage(
                    this.cloudinary,
                    string.Format(GlobalConstants.StickerTypeName, targetStickerType.Id),
                    GlobalConstants.StickerTypeFolder);

                this.db.FavouriteStickers.RemoveRange(favouriteStickerTypes);
                this.db.Stickers.RemoveRange(allStickers);
                this.db.StickerTypes.Remove(targetStickerType);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(SuccessMessages.SuccessfullyDeleteChatStickerType, name.ToUpper(), count));
            }

            return Tuple.Create(false, ErrorMessages.StickerTypeDoesNotExist);
        }

        public ICollection<DeleteChatStickerTypeViewModel> GetAllStickersTypes()
        {
            var result = new List<DeleteChatStickerTypeViewModel>();
            var allStickersTypes = this.db.StickerTypes.OrderBy(x => x.Name).ToList();

            foreach (var stickerType in allStickersTypes)
            {
                result.Add(new DeleteChatStickerTypeViewModel
                {
                    Id = stickerType.Id,
                    Name = stickerType.Name,
                });
            }

            return result;
        }

        public List<string> GetStickersUrls(string stickerTypeId)
        {
            var targetStickersUrls = this.db.Stickers.Where(x => x.StickerTypeId == stickerTypeId).Select(x => x.Url).ToList();

            return targetStickersUrls;
        }
    }
}
