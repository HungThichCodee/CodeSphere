using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.EditChatSticker
{
    public class EditChatStickerRepository : IEditChatStickerRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public EditChatStickerRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> EditSticker(EditChatStickerInputModel model)
        {
            var targetStickerType =
                await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == model.StickerTypeId);

            if (targetStickerType != null)
            {
                if (this.db.Stickers.Any(x => x.Name.ToUpper() == model.Name.ToUpper() && x.StickerTypeId == model.StickerTypeId))
                {
                    return Tuple.Create(
                        false,
                        string.Format(ErrorMessages.StickerAlreadyExist, model.Name.ToUpper()));
                }

                var targetSticker = await this.db.Stickers.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (targetSticker != null)
                {
                    targetSticker.StickerTypeId = model.StickerTypeId;
                    targetSticker.Name = model.Name;
                    if (model.Image != null)
                    {
                        var imageUrl = await ApplicationCloudinary.UploadImage(
                            this.cloudinary,
                            model.Image,
                            string.Format(GlobalConstants.StickerName, model.Id),
                            GlobalConstants.StickersFolder);

                        targetSticker.Url = imageUrl;
                    }

                    this.db.Stickers.Update(targetSticker);
                    await this.db.SaveChangesAsync();

                    return Tuple.Create(
                        true,
                        string.Format(
                            SuccessMessages.SuccessfullyEditChatSticker,
                            targetSticker.Name.ToUpper()));
                }

                return Tuple.Create(false, ErrorMessages.StickerDoesNotExist);
            }

            return Tuple.Create(false, ErrorMessages.StickerTypeDoesNotExist);
        }

        public ICollection<EditChatStickerViewModel> GetAllStickers()
        {
            var result = new List<EditChatStickerViewModel>();
            var targetStickers = this.db.Stickers.OrderBy(x => x.Name).ToList();

            foreach (var sticker in targetStickers)
            {
                result.Add(new EditChatStickerViewModel
                {
                    Id = sticker.Id,
                    Name = sticker.Name,
                });
            }

            return result;
        }

        public ICollection<EditStickerStickerTypeViewModel> GetAllStikersTypes()
        {
            var result = new List<EditStickerStickerTypeViewModel>();
            var allStickersTypes = this.db.StickerTypes.OrderBy(x => x.Name).ToList();

            foreach (var stickerType in allStickersTypes)
            {
                result.Add(new EditStickerStickerTypeViewModel
                {
                    Id = stickerType.Id,
                    Name = stickerType.Name,
                });
            }

            return result;
        }

        public async Task<GetEditChatStickerDataViewModel> GetStickerById(string stickerId)
        {
            var targetSticker = await this.db.Stickers.FirstOrDefaultAsync(x => x.Id == stickerId);
            return new GetEditChatStickerDataViewModel
            {
                Name = targetSticker.Name,
                Url = targetSticker.Url,
                StickerTypeId = targetSticker.StickerTypeId,
            };
        }
    }
}