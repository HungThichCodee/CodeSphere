using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddChatStickerType.InputModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddChatStickerType
{
    public class AddChatStickerTypeRepository : IAddChatStickerTypeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddChatStickerTypeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> AddNewStickerType(AddChatStickerTypeInputModel model)
        {
            if (this.db.StickerTypes.Any(x => x.Name.ToUpper() == model.Name.ToUpper()))
            {
                return Tuple.Create(
                    false,
                    string.Format(ErrorMessages.StickerTypeAlreadyExist, model.Name.ToUpper()));
            }

            var stickerType = new StickerType
            {
                Name = model.Name,
                Position = await this.db.StickerTypes
                    .Select(x => x.Position)
                    .OrderByDescending(x => x)
                    .FirstOrDefaultAsync() + 1,
            };

            var imageUrl = await ApplicationCloudinary.UploadImage(
                this.cloudinary,
                model.Image,
                string.Format(GlobalConstants.StickerTypeName, stickerType.Id),
                GlobalConstants.StickerTypeFolder);

            stickerType.Url = imageUrl;

            this.db.StickerTypes.Add(stickerType);
            await this.db.SaveChangesAsync();

            return Tuple.Create(
                true,
                string.Format(SuccessMessages.SuccessfullyAddedStickerType, stickerType.Name.ToUpper()));
        }
    }
}
