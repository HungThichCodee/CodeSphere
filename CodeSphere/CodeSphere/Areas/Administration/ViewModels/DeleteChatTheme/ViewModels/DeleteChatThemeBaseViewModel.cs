using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.ViewModels
{
    public class DeleteChatThemeBaseViewModel
    {
        public ICollection<DeleteChatThemeViewModel> DeleteChatThemeViewModels { get; set; } = new HashSet<DeleteChatThemeViewModel>();

        public DeleteChatThemeInputModel DeleteChatThemeInputModel { get; set; } =
            new DeleteChatThemeInputModel();
    }
}