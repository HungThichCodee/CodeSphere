using CodeSphere.Areas.Administration.ViewModels.AddHolidayTheme.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddHolidayTheme
{
    public interface IAddHolidayThemeRepository
    {
        Task<Tuple<bool, string>> AddNewHolidayTheme(AddHolidayThemeInputModel model);
    }
}
