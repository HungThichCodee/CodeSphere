using CodeSphere.Areas.Administration.ViewModels.AllHolidayThemes.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.AllHolidayThemes
{
    public interface IAllHolidayThemesRepository
    {
        ICollection<AllHolidayThemesViewModel> GetAllHolidayThemes();

        Task<Tuple<bool, string>> ChangeHolidayThemeStatus(string id, bool status);

        Task<Tuple<bool, string>> DeleteHolidayTheme(string id);
    }
}
