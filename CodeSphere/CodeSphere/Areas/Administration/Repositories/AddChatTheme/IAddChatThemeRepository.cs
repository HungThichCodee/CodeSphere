using CodeSphere.Areas.Administration.ViewModels.AddChatTheme.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddChatTheme
{
    public interface IAddChatThemeRepository
    {
        Task<Tuple<bool, string>> AddChatTheme(AddChatThemeInputModel model);
    }
}
