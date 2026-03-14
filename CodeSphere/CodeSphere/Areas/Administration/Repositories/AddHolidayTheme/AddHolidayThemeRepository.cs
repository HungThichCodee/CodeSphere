using CloudinaryDotNet;
using CodeSphere.Areas.Administration.Models.HolidayTheme;
using CodeSphere.Areas.Administration.ViewModels.AddHolidayTheme.InputModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddHolidayTheme
{
    public class AddHolidayThemeRepository : IAddHolidayThemeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddHolidayThemeRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> AddNewHolidayTheme(AddHolidayThemeInputModel model)
        {
            var targetTheme = await this.db.HolidayThemes
                .FirstOrDefaultAsync(x => x.Name.ToUpper() == model.Name.ToUpper());

            if (targetTheme == null)
            {
                targetTheme = new HolidayTheme
                {
                    Name = model.Name,
                    IsActive = false,
                };

                foreach (var icon in model.Icons)
                {
                    var targetIcon = new HolidayIcon
                    {
                        Name = Path.GetFileNameWithoutExtension(icon.FileName),
                    };

                    var iconUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        icon,
                        string.Format(GlobalConstants.HolidayIconName, targetIcon.Id),
                        GlobalConstants.HolidayThemesFolder);

                    targetIcon.Url = iconUrl;
                    targetTheme.HolidayIcons.Add(targetIcon);
                }

                this.db.HolidayThemes.Add(targetTheme);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(SuccessMessages.SuccessfullyAddedHolidayTheme, targetTheme.Name.ToUpper()));
            }

            return Tuple.Create(
                false,
                string.Format(ErrorMessages.HolidayThemeAlreadyExist, model.Name.ToUpper()));
        }
    }
}
