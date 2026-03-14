using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.ViewModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddChatSticker
{
    public class AddChatStickerRepository : IAddChatStickerRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddChatStickerRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> AddNewSticker(AddChatStickerInputModel model)
        {
            var targetType = await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == model.StickerTypeId);

            if (targetType != null)
            {
                var sticker = await this.db.Stickers
                    .FirstOrDefaultAsync(x => x.Name.ToUpper() == model.Name.ToUpper() && x.StickerTypeId == model.StickerTypeId);

                if (sticker == null)
                {
                    sticker = new Sticker
                    {
                        Name = model.Name,
                        StickerTypeId = targetType.Id,
                        Position = await this.db.Stickers
                            .Where(x => x.StickerTypeId == targetType.Id)
                            .Select(x => x.Position).OrderByDescending(x => x)
                            .FirstOrDefaultAsync() + 1,
                    };

                    var imageUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        model.Image,
                        string.Format(GlobalConstants.StickerName, sticker.Id),
                        GlobalConstants.StickersFolder);

                    sticker.Url = imageUrl;

                    this.db.Stickers.Add(sticker);
                    await this.db.SaveChangesAsync();

                    return Tuple.Create(
                        true,
                        string.Format(SuccessMessages.SuccessfullyAddedSticker, sticker.Name.ToUpper()));
                }

                return Tuple.Create(
                    false,
                    string.Format(ErrorMessages.StickerAlreadyExist, model.Name.ToUpper()));
            }

            return Tuple.Create(false, ErrorMessages.StickerTypeDoesNotExist);
        }

        public ICollection<AddChatStickerViewModel> GetAllStickerTypes()
        {
            var result = new List<AddChatStickerViewModel>();
            var allTypes = this.db.StickerTypes.OrderBy(x => x.Position).ToList();

            foreach (var currentType in allTypes)
            {
                result.Add(new AddChatStickerViewModel
                {
                    Id = currentType.Id,
                    Name = currentType.Name,
                    Url = currentType.Url,
                });
            }

            return result;
        }
    }
}
