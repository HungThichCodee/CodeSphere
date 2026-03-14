using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AllHolidayThemes.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AllHolidayThemes
{
    public class AllHolidayThemesRepository : IAllHolidayThemesRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AllHolidayThemesRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> ChangeHolidayThemeStatus(string id, bool status)
        {
            var targetTheme = await this.db.HolidayThemes.FirstOrDefaultAsync(x => x.Id == id);

            if (targetTheme != null)
            {
                targetTheme.IsActive = status;
                this.db.HolidayThemes.Update(targetTheme);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(
                        SuccessMessages.SuccessfullyEditHolidayThemeStatus,
                        targetTheme.Name.ToUpper(),
                        status.ToString().ToUpper()));
            }

            return Tuple.Create(false, ErrorMessages.HolidayThemeDoesNotExist);
        }

        public async Task<Tuple<bool, string>> DeleteHolidayTheme(string id)
        {
            var targetTheme = await this.db.HolidayThemes.FirstOrDefaultAsync(x => x.Id == id);

            if (targetTheme != null)
            {
                var themeName = targetTheme.Name;

                var allThemeIcons = this.db.HolidayIcons.Where(x => x.HolidayThemeId == targetTheme.Id).ToList();

                foreach (var icon in allThemeIcons)
                {
                    ApplicationCloudinary.DeleteImage(
                        this.cloudinary,
                        string.Format(GlobalConstants.HolidayIconName, icon.Id),
                        GlobalConstants.HolidayThemesFolder);
                }

                this.db.HolidayIcons.RemoveRange(allThemeIcons);
                this.db.HolidayThemes.Remove(targetTheme);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(SuccessMessages.SuccessfullyDeleteHolidayTheme, themeName.ToUpper()));
            }

            return Tuple.Create(false, ErrorMessages.HolidayThemeDoesNotExist);
        }

        public ICollection<AllHolidayThemesViewModel> GetAllHolidayThemes()
        {
            var result = new List<AllHolidayThemesViewModel>();

            var allThemes = this.db.HolidayThemes.OrderBy(x => x.Name).ToList();

            foreach (var theme in allThemes)
            {
                result.Add(new AllHolidayThemesViewModel
                {
                    Id = theme.Id,
                    Name = theme.Name,
                    IsActive = theme.IsActive,
                    IconsUrls = this.db.HolidayIcons
                        .Where(x => x.HolidayThemeId == theme.Id)
                        .Select(x => x.Url)
                        .ToList(),
                });
            }

            return result;
        }
    }
}
