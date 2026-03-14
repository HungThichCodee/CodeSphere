using CodeSphere.Constraints;
using CodeSphere.Models.User;
using CodeSphere.Repositories.ProfileRepositories.Pagination.Profile;
using CodeSphere.ViewModels.Pagination.Profile;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.Profile
{
    public class FollowingViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfileFollowingRepository followingRepository;

        public FollowingViewComponent(UserManager<ApplicationUser> userManager, IProfileFollowingRepository followingRepository)
        {
            this.userManager = userManager;
            this.followingRepository = followingRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            List<FollowingViewModel> allFollowing = await this.followingRepository.ExtractFollowing(username);

            FollowingPaginationViewModel model = new FollowingPaginationViewModel
            {
                Username = username,
                Followings = allFollowing.ToPagedList(page, GlobalConstants.FollowingCountOnPage),
            };

            return View(model);
        }
    }
}
