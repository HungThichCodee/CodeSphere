using CodeSphere.ViewModels.Users.InputModels;

namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class ManageAccountBaseModel
    {
        public ManageAccountViewModel ManageAccountViewModel { get; set; } = new ManageAccountViewModel();

        public ManageAccountInputModel ManageAccountInputModel { get; set; } = new ManageAccountInputModel();
    }
}