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
    public class BannedPostsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfileBannedPostsRepository bannedPostsRepository;

        public BannedPostsViewComponent(UserManager<ApplicationUser> userManager, IProfileBannedPostsRepository bannedPostsRepository)
        {
            this.userManager = userManager;
            this.bannedPostsRepository = bannedPostsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            var user = await userManager.FindByNameAsync(username);
            var currentUserId = userManager.GetUserId(HttpContext.User);
            List<BannedPostViewModel> allBannedPosts = await bannedPostsRepository.ExtractBannedPosts(user, currentUserId);

            BannedPostsPaginationViewModel model = new BannedPostsPaginationViewModel
            {
                Username = username,
                BannedPosts = allBannedPosts.ToPagedList(page, GlobalConstants.BannedPostsCountOnPage),
            };

            return View(model);
        }
    }
}
