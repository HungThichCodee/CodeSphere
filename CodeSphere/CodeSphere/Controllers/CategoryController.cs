using CodeSphere.Constraints;
using CodeSphere.Models.User;
using CodeSphere.Repositories.CategoryRepositories;
using CodeSphere.ViewModels.CategoryViewModels;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.CategoryPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoryController(ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        /// <summary>
        /// This function will get all categories with there related Blog Posts.
        /// </summary>
        /// <param name="id">Target Category ID.</param>
        /// <param name="page">Target page for displayed items.</param>
        /// <returns>Return a View with a View Model with all Categories and there Blog Posts.</returns>
        [HttpGet]
        [Authorize]
        [Route("Blog/Category/{id}/{page?}")]
        public async Task<IActionResult> Index(string id, int? page)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var pageNumber = page ?? 1;
            var posts = await this.categoryRepository.ExtractPostsByCategoryId(id, currentUser);

            CategoryPageViewModel model = new CategoryPageViewModel
            {
                Category = await this.categoryRepository.ExtractCategoryById(id),
                Posts = posts.ToPagedList(pageNumber, GlobalConstants.BlogPostsOnPage),
            };

            return this.View(model);
        }
    }
}
