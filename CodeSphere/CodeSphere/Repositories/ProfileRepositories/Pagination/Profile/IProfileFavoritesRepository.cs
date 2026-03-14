using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfileFavoritesRepository
    {
        List<FavouritePostViewModel> ExtractFavorites(ApplicationUser user, ApplicationUser currentUser);
    }
}
