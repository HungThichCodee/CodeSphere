using CodeSphere.Areas.Administration.ViewModels.EditChatTheme.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.EditChatTheme.ViewModels
{
    public class EditChatThemeBaseModel
    {
        public ICollection<EditChatThemeViewModel> EditChatThemeViewModels { get; set; } =
            new HashSet<EditChatThemeViewModel>();

        public EditChatThemeInputModel EditChatThemeInput { get; set; } = new EditChatThemeInputModel();
    }
}