using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatTheme
{
    public class DeleteChatThemeRepository : IDeleteChatThemeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public DeleteChatThemeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> DeleteChatTheme(DeleteChatThemeInputModel model)
        {
            var targetTheme = await this.db.ChatThemes.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (targetTheme == null)
            {
                return Tuple.Create(
                    false,
                    string.Format(ErrorMessages.ChatThemeDoesNotAlreadyExist, model.Name.ToUpper()));
            }

            ApplicationCloudinary.DeleteImage(
                this.cloudinary,
                string.Format(GlobalConstants.ChatThemeName, targetTheme.Id),
                GlobalConstants.ChatThemesFolderName);
            var targetGroups = this.db.Groups.Where(x => x.ChatThemeId == targetTheme.Id);

            foreach (var group in targetGroups)
            {
                group.ChatThemeId = null;
            }

            this.db.Groups.UpdateRange(targetGroups);
            await this.db.SaveChangesAsync();
            this.db.ChatThemes.Remove(targetTheme);
            await this.db.SaveChangesAsync();

            return Tuple.Create(
                true,
                string.Format(SuccessMessages.SuccessfullyDeleteChatTheme, model.Name.ToUpper()));
        }

        public ICollection<DeleteChatThemeViewModel> GetAllChatThemes()
        {
            var result = new List<DeleteChatThemeViewModel>();

            foreach (var theme in this.db.ChatThemes.ToList())
            {
                result.Add(new DeleteChatThemeViewModel
                {
                    Id = theme.Id,
                    Name = theme.Name,
                });
            }

            return result;
        }

        public async Task<GetDeleteChatThemeDataViewModel> GetThemeById(string themeId)
        {
            var theme = await this.db.ChatThemes.FirstOrDefaultAsync(x => x.Id == themeId);

            return new GetDeleteChatThemeDataViewModel
            {
                Name = theme.Name,
                ImageUrl = theme.Url,
            };
        }
    }
}
