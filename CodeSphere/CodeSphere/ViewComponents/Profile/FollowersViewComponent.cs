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
    public class FollowersViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfileFollowersRepository followersRepository;

        public FollowersViewComponent(UserManager<ApplicationUser> userManager, IProfileFollowersRepository followersRepository)
        {
            this.userManager = userManager;
            this.followersRepository = followersRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            List<FollowersViewModel> allFollowers = await this.followersRepository.ExtractFollowers(username);

            FollowersPaginationViewModel model = new FollowersPaginationViewModel
            {
                Username = username,
                Followers = allFollowers.ToPagedList(page, GlobalConstants.FollowersCountOnPage),
            };

            return View(model);
        }
    }
}
