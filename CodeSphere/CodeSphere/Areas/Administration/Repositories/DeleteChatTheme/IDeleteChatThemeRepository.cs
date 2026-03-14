using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.DeleteChatTheme
{
    public interface IDeleteChatThemeRepository
    {
        ICollection<DeleteChatThemeViewModel> GetAllChatThemes();

        Task<GetDeleteChatThemeDataViewModel> GetThemeById(string themeId);

        Task<Tuple<bool, string>> DeleteChatTheme(DeleteChatThemeInputModel model);
    }
}
