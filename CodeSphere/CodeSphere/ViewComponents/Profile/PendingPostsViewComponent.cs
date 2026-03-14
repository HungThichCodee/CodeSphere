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
    public class PendingPostsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProfilePendingPostsRepository pendingPostsRepository;

        public PendingPostsViewComponent(UserManager<ApplicationUser> userManager, IProfilePendingPostsRepository pendingPostsRepository)
        {
            this.userManager = userManager;
            this.pendingPostsRepository = pendingPostsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            var user = await userManager.FindByNameAsync(username);
            var currentUserId = userManager.GetUserId(HttpContext.User);
            List<PendingPostViewModel> allPendingPosts = await this.pendingPostsRepository.ExtractPendingPosts(user, currentUserId);

            PendingPostsPaginationViewModel model = new PendingPostsPaginationViewModel
            {
                Username = username,
                PendingPosts = allPendingPosts.ToPagedList(page, GlobalConstants.PendingPostsCountOnPage),
            };

            return View(model);
        }
    }
}
