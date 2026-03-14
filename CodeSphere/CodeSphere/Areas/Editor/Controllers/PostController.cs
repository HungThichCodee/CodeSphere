using CodeSphere.ApplicationAttributes.ActionAttributes;
using CodeSphere.Areas.Editor.Repositories.PostRepositories;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Editor.Controllers
{
    [Authorize(Roles = GlobalConstants.EditorRole + "," + GlobalConstants.AdministratorRole)]
    [Area(GlobalConstants.EditorArea)]
    public class PostController : Controller
    {
        private readonly IEditorPostRepository postRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public PostController(
            IEditorPostRepository postRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            this.postRepository = postRepository;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }

        [UserBlocked("Index", "Profile")]
        public async Task<IActionResult> ApprovePost(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.contextAccessor.HttpContext.User);

            bool isApproved = await this.postRepository.ApprovePost(id, currentUser);
            if (isApproved)
            {
                this.TempData["Success"] = SuccessMessages.SuccessfullyApprovedBlogPost;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            }

            return this.RedirectToAction("Index", "Post", new { postId = id });
        }

        [UserBlocked("Index", "Profile")]
        public async Task<IActionResult> UnbanPost(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.contextAccessor.HttpContext.User);

            bool isUnbanned = await this.postRepository.UnbannPost(id, currentUser);
            if (isUnbanned)
            {
                this.TempData["Success"] = SuccessMessages.SuccessfullyUnannedBlogPost;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            }

            return this.RedirectToAction("Index", "Post", new { postId = id });
        }

        [UserBlocked("Index", "Profile")]
        public async Task<IActionResult> BanPost(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.contextAccessor.HttpContext.User);

            bool isBanned = await this.postRepository.BannPost(id, currentUser);
            if (isBanned)
            {
                this.TempData["Success"] = SuccessMessages.SuccessfullyBannedBlogPost;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            }

            return this.RedirectToAction("Index", "Post", new { postId = id });
        }
    }
}
