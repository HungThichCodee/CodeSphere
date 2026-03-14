using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddChatTheme.InputModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddChatTheme
{
    public class AddChatThemeRepository : IAddChatThemeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddChatThemeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> AddChatTheme(AddChatThemeInputModel model)
        {
            var targetTheme = await this.db.ChatThemes
                .FirstOrDefaultAsync(x => x.Name.ToUpper() == model.Name.ToUpper());

            if (targetTheme != null)
            {
                return Tuple.Create(
                    false,
                    string.Format(ErrorMessages.ChatThemeAlreadyExist, model.Name.ToUpper()));
            }

            targetTheme = new ChatTheme
            {
                Name = model.Name,
            };
            var imageUrl = await ApplicationCloudinary.UploadImage(
                this.cloudinary,
                model.Image,
                string.Format(GlobalConstants.ChatThemeName, targetTheme.Id),
                GlobalConstants.ChatThemesFolderName);
            targetTheme.Url = imageUrl;

            this.db.ChatThemes.Add(targetTheme);
            await this.db.SaveChangesAsync();

            return Tuple.Create(
                true,
                string.Format(SuccessMessages.SuccessfullyAddedChatTheme, model.Name.ToUpper()));
        }
    }
}
