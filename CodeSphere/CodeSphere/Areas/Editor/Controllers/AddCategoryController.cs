using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels;
using CodeSphere.Areas.Editor.Repositories.CategoryRepositories;
using CodeSphere.Areas.Editor.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Editor.Controllers
{
    [Authorize(Roles = GlobalConstants.EditorRole + "," + GlobalConstants.AdministratorRole)]
    [Area(GlobalConstants.EditorArea)]
    public class AddCategoryController : Controller
    {
        private readonly IAddCategoryRepository addCategoryRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AddCategoryController(
            IAddCategoryRepository addCategoryRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.addCategoryRepository = addCategoryRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.IsBlocked)
            {
                this.TempData["Error"] = ErrorMessages.YouAreBlock;
                return this.RedirectToAction("Index", "Blog");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddCategoryInputModel model)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.IsBlocked)
            {
                this.TempData["Error"] = ErrorMessages.YouAreBlock;
                return this.RedirectToAction("Index", "Blog");
            }

            if (this.ModelState.IsValid)
            {
                var tuple = await this.addCategoryRepository
                    .CreateCategory(model.Name, model.Description = model.SanitizedDescription);
                this.TempData[tuple.Item1] = tuple.Item2;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "Blog", model);
            }

            return this.RedirectToAction("Index", "Blog");
        }
    }
}
