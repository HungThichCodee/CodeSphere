using CodeSphere.Areas.Administration.ViewModels.EditChatTheme.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatTheme.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.EditChatTheme
{
    public interface IEditChatThemeRepository
    {
        ICollection<EditChatThemeViewModel> GetAllThemes();

        Task<GetEditChatThemeDataViewModel> GetThemeById(string themeId);

        Task<Tuple<bool, string>> EditChatTheme(EditChatThemeInputModel model);
    }
}