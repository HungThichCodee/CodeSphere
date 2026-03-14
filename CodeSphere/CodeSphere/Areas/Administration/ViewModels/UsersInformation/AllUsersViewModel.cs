using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Areas.Administration.ViewModels.UsersInformation
{
    public class AllUsersViewModel
    {
        public ICollection<ApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ApplicationUserViewModel>();
    }
}
