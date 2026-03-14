using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.ViewModels.Pagination.Profile
{
    public class FavoritesPaginationViewModel
    {
        public string? Username { get; set; }

        public IEnumerable<FavouritePostViewModel> Favorites { get; set; } = new HashSet<FavouritePostViewModel>();
    }
}
