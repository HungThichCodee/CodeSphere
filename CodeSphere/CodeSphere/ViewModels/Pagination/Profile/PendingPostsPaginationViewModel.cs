using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class PendingPostsPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<PendingPostViewModel> PendingPosts { get; set; } = new HashSet<PendingPostViewModel>();
    }
}
