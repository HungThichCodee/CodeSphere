using CodeSphere.Models.User;

namespace CodeSphere.ViewModels.Home
{
    public class HomeViewModel
    {
        public int TotalRegisteredUsers { get; set; }

        public int TotalBlogPosts { get; set; }

        public ICollection<HomeAdministratorUserViewModel> Administrators { get; set; } = new HashSet<HomeAdministratorUserViewModel>();
    }
}
