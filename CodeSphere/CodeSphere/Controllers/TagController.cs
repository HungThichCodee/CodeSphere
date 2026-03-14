using CodeSphere.Constraints;
using CodeSphere.Models.User;
using CodeSphere.Repositories.TagRepositories;
using CodeSphere.ViewModels.TagViewModels;
using CodeSphere.ViewModels.TagViewModels.TagPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public TagController(ITagRepository tagRepository, UserManager<ApplicationUser> userManager)
        {
            this.tagRepository = tagRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("Blog/Tag/{id}/{page?}")]
        public async Task<IActionResult> Index(string id, int? page)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var pageNumber = page ?? 1;
            var post = await this.tagRepository.ExtractPostsByTagId(id, currentUser);

            TagPageViewModel model = new TagPageViewModel
            {
                Tag = await this.tagRepository.ExtractTagById(id),
                Posts = post.ToPagedList(pageNumber, GlobalConstants.BlogPostsOnPage),
            };

            return this.View(model);
        }
    }
}
