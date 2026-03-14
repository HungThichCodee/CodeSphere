using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.EditChatStickerType
{
    public class EditChatStickerTypeRepository : IEditChatStickerTypeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public EditChatStickerTypeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> EditStickerType(EditChatStickerTypeInputModel model)
        {
            if (this.db.StickerTypes.Any(x => x.Name.ToUpper() == model.Name.ToUpper()))
            {
                return Tuple.Create(false, string.Format(ErrorMessages.StickerAlreadyTypeExist, model.Name.ToUpper()));
            }

            var targetStickerType = await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (targetStickerType != null)
            {
                targetStickerType.Name = model.Name;
                if (model.Image != null)
                {
                    var imageUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        model.Image,
                        string.Format(GlobalConstants.StickerTypeName, model.Id),
                        GlobalConstants.StickerTypeFolder);

                    targetStickerType.Url = imageUrl;
                }

                this.db.StickerTypes.Update(targetStickerType);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(
                        SuccessMessages.SuccessfullyEditChatStickerType,
                        targetStickerType.Name.ToUpper()));
            }

            return Tuple.Create(false, ErrorMessages.StickerTypeDoesNotExist);
        }

        public ICollection<EditChatStickerTypeViewModel> GetAllChatStickerTypes()
        {
            var result = new List<EditChatStickerTypeViewModel>();
            var allStickerTypes = this.db.StickerTypes.OrderBy(x => x.Name).ToList();

            foreach (var stickerType in allStickerTypes)
            {
                result.Add(new EditChatStickerTypeViewModel
                {
                    Id = stickerType.Id,
                    Name = stickerType.Name,
                });
            }

            return result;
        }

        public async Task<GetEditChatStickerTypeDataViewModel> GetStickerTypeById(string stickerTypeId)
        {
            var targetStickerType = await this.db.StickerTypes.FirstOrDefaultAsync(x => x.Id == stickerTypeId);
            return new GetEditChatStickerTypeDataViewModel
            {
                Name = targetStickerType.Name,
                Url = targetStickerType.Url,
            };
        }
    }
}