using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.ViewModels.Pagination.AllUsers
{
    public class BannedUsersPaginationViewModel
    {
        public IEnumerable<AllUsersUserCardViewModel> AllUsers { get; set; } = new HashSet<AllUsersUserCardViewModel>();
    }
}
