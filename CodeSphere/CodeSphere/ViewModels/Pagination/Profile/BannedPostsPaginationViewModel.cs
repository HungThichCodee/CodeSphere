using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class BannedPostsPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<BannedPostViewModel> BannedPosts { get; set; } = new HashSet<BannedPostViewModel>();
    }
}
