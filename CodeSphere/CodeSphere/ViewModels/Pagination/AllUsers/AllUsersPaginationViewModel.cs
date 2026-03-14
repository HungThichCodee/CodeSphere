using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.ViewModels.Pagination.AllUsers
{
    public class AllUsersPaginationViewModel
    {
        public IEnumerable<AllUsersUserCardViewModel> AllUsers { get; set; } = new HashSet<AllUsersUserCardViewModel>();
    }
}
