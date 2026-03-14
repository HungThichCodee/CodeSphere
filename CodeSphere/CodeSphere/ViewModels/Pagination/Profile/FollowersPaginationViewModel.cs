using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class FollowersPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<FollowersViewModel> Followers { get; set; } = new HashSet<FollowersViewModel>();
    }
}
