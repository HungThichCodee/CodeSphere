using CodeSphere.Constraints;
using CodeSphere.Repositories.AllCategories;
using CodeSphere.ViewModels.AllCategories.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    [Authorize]
    public class AllCategoriesController : Controller
    {
        private readonly IAllCategoriesRepository allCategoriesRepository;

        public AllCategoriesController(IAllCategoriesRepository allCategoriesRepository)
        {
            this.allCategoriesRepository = allCategoriesRepository;
        }

        /// <summary>
        /// This function will return a list of all Categories with there TOP Blog Posts.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <returns>Returns a view with a collection of all Categories with there Posts.</returns>
        [HttpGet]
        [Route("AllCategories/{page?}")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            IEnumerable<AllCategoriesCategoryViewModel> model = this.allCategoriesRepository.GetAllBlogCategories();
            return this.View(model.ToPagedList(pageNumber, GlobalConstants.BlogCategoriesOnPage));
        }
    }
}
