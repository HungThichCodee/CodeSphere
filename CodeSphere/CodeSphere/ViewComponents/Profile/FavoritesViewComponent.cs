using CodeSphere.Constraints;
using CodeSphere.Models.User;
using CodeSphere.Repositories.ProfileRepositories.Pagination.Profile;
using CodeSphere.ViewModels.Pagination.Profile;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.Profile
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfileFavoritesRepository favoritesRepository;

        public FavoritesViewComponent(UserManager<ApplicationUser> userManager, IProfileFavoritesRepository favoritesRepository)
        {
            this.userManager = userManager;
            this.favoritesRepository = favoritesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            var user = await userManager.FindByNameAsync(username);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            List<FavouritePostViewModel> allFollowers = this.favoritesRepository.ExtractFavorites(user, currentUser);

            FavoritesPaginationViewModel model = new FavoritesPaginationViewModel
            {
                Username = username,
                Favorites = allFollowers.ToPagedList(page, GlobalConstants.FavoritesCountOnPage),
            };

            return View(model);
        }
    }
}
