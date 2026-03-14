using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class FollowingPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<FollowingViewModel> Followings { get; set; } = new HashSet<FollowingViewModel>();
    }
}
