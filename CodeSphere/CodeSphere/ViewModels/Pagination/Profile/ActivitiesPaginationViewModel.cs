using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class ActivitiesPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<ActivitiesViewModel> Activities { get; set; } = new HashSet<ActivitiesViewModel>();
    }
}
