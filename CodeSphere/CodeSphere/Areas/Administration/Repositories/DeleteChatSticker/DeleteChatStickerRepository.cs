using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatSticker
{
    public class DeleteChatStickerRepository : IDeleteChatStickerRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public DeleteChatStickerRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> DeleteChatSticker(DeleteChatStickerInputModel model)
        {
            var targetSticker = await this.db.Stickers.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (targetSticker != null)
            {
                string name = targetSticker.Name;
                ApplicationCloudinary.DeleteImage(
                        this.cloudinary,
                        string.Format(GlobalConstants.StickerName, model.Id),
                        GlobalConstants.StickersFolder);

                this.db.Stickers.Remove(targetSticker);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(SuccessMessages.SuccessfullyDeleteChatSticker, name.ToUpper()));
            }

            return Tuple.Create(false, ErrorMessages.StickerDoesNotExist);
        }

        public ICollection<DeleteChatStickerViewModel> GetAllStickers()
        {
            var result = new List<DeleteChatStickerViewModel>();
            var allStickers = this.db.Stickers.OrderBy(x => x.Name).ToList();

            foreach (var sticker in allStickers)
            {
                result.Add(new DeleteChatStickerViewModel
                {
                    Id = sticker.Id,
                    Name = sticker.Name,
                    Url = sticker.Url,
                });
            }

            return result;
        }

        public async Task<string> GetStickerUrl(string stickerId)
        {
            var targetStickerUrl = await this.db.Stickers
                .Where(x => x.Id == stickerId)
                .Select(x => x.Url)
                .FirstOrDefaultAsync();

            return targetStickerUrl;
        }
    }
}
