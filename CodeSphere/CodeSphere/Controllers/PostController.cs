using CodeSphere.ApplicationAttributes.ActionAttributes;
using CodeSphere.ApplicationAttributes.Blog;
using CodeSphere.ApplicationAttributes.Blog.Post;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.Repositories.PostRepositories;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PostController(
            IPostRepository postRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.postRepository = postRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("/Blog/Post/{postId}")]
        [UserBlocked("Index", "Profile")]
        [BlogRole("Index", "Blog")]
        public async Task<IActionResult> Index(string postId)
        {
            if (!await this.postRepository.IsPostExist(postId))
            {
                return this.NotFound();
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            PostViewModel model = await this.postRepository.ExtractCurrentPost(postId, currentUser);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Blog/Post/LikePost/{postId}")]
        [UserBlocked("Index", "Profile")]
        [PostActions("Index", "Blog", null, ErrorMessages.CannotLikeNotApprovedBlogPost)]
        public async Task<IActionResult> LikePost(string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tuple = await this.postRepository.LikePost(postId, currentUser);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Post", new { postId });
        }

        [HttpPost]
        [Authorize]
        [Route("/Blog/Post/UnlikePost/{postId}")]
        [UserBlocked("Index", "Profile")]
        [PostActions("Index", "Blog", null, ErrorMessages.CannotUnlikeNotApprovedBlogPost)]
        public async Task<IActionResult> UnlikePost(string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tuple = await this.postRepository.UnlikePost(postId, currentUser);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Post", new { postId });
        }

        [HttpPost]
        [Authorize]
        [UserBlocked("Index", "Profile")]
        [PostActions("Index", "Blog", null, ErrorMessages.CannotAddToFavoriteNotApprovedBlogPost)]
        public async Task<IActionResult> AddToFavorite(string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tuple = await this.postRepository.AddToFavorite(currentUser, postId);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Post", new { postId });
        }

        [HttpPost]
        [Authorize]
        [UserBlocked("Index", "Profile")]
        [PostActions("Index", "Blog", null, ErrorMessages.CannotRemoveFromFavoriteNotApprovedBlogPost)]
        public async Task<IActionResult> RemoveFromFavorite(string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tuple = await this.postRepository.RemoveFromFavorite(currentUser, postId);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Post", new { postId });
        }
    }
}
