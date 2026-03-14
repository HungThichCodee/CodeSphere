using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.ViewModels.Pagination.AllUsers
{
    public class AllAdministratorsPaginationViewModel
    {
        public IEnumerable<AllUsersUserCardViewModel> AllUsers { get; set; } = new HashSet<AllUsersUserCardViewModel>();
    }
}
