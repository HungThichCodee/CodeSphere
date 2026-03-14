using CodeSphere.Constraints;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.Repositories.UserPostRepositories;
using CodeSphere.ViewModels.UserPostsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    public class UserPostsController : Controller
    {
        private readonly IUserPostsRepository userPostsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserPostsController(IUserPostsRepository userPostsRepository, UserManager<ApplicationUser> userManager)
        {
            this.userPostsRepository = userPostsRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("Blog/UserPosts/{username}/{filter}/{page?}")]
        public async Task<IActionResult> Index(string username, string filter, int? page)
        {
            UserPostsViewModel model = new UserPostsViewModel
            {
                Username = username,
            };

            var pageNumber = page ?? 1;

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (filter == UserPostsFilter.Liked.ToString())
            {
                model.Action = UserPostsFilter.Liked.ToString();
                var posts = await this.userPostsRepository.ExtractLikedPostsByUsername(username, currentUser);
                model.Posts = posts.ToPagedList(pageNumber, GlobalConstants.BlogPostsOnPage);
            }
            else if (filter == UserPostsFilter.Created.ToString())
            {
                model.Action = UserPostsFilter.Created.ToString();
                var posts = await this.userPostsRepository.ExtractCreatedPostsByUsername(username, currentUser);
                model.Posts = posts.ToPagedList(pageNumber, GlobalConstants.BlogPostsOnPage);
            }

            return this.View(model);
        }
    }
}
