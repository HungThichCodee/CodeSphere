using CodeSphere.Areas.Administration.Repositories.BlogAddons;
using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels;
using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.InputModels;
using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.ViewModels;
using CodeSphere.Areas.Editor.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class BlogAddonsController : Controller
    {
        private readonly IBlogAddonsRepository addonsRepository;

        public BlogAddonsController(IBlogAddonsRepository addonsRepository)
        {
            this.addonsRepository = addonsRepository;
        }

        public IActionResult AddTag()
        {
            var model = new AddRemoveTagBaseModel
            {
                AddRemoveTagInputModel = new AddRemoveTagInputModel(),
                TagsNames = this.addonsRepository.GetAllTags(),
            };
            return this.View(model);
        }

        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(AddCategoryInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var tuple = await this.addonsRepository
                    .CreateCategoryAdminArea(model.Name, model.Description = model.SanitizedDescription);
                this.TempData[tuple.Item1] = tuple.Item2;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("AddCategory", "BlogAddons", model);
            }

            return this.RedirectToAction("AddCategory", "BlogAddons");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTag(AddRemoveTagBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                var tuple = await this.addonsRepository.CreateTag(model.AddRemoveTagInputModel.Name);
                this.TempData[tuple.Item1] = tuple.Item2;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("AddTag", "BlogAddons", model);
            }

            return this.RedirectToAction("AddTag", "BlogAddons");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTag(AddRemoveTagBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                var tuple = await this.addonsRepository.RemoveTag(model.AddRemoveTagInputModel.Name);
                this.TempData[tuple.Item1] = tuple.Item2;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("AddTag", "BlogAddons", model);
            }

            return this.RedirectToAction("AddTag", "BlogAddons");
        }

        public IActionResult EditCategory()
        {
            var model = new EditCategoryBaseModel
            {
                EditCategoryInputModel = new EditCategoryInputModel(),
                EditCategoryViewModels = this.addonsRepository.GetAllCategories(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditExistingCategory(EditCategoryBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.addonsRepository.EditExistingCategory(model.EditCategoryInputModel);
                this.TempData["Success"] = string.Format(
                    SuccessMessages.SuccessfullyEditCategory,
                    model.EditCategoryInputModel.Name.ToUpper());
                return this.RedirectToAction("EditCategory", "BlogAddons");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("EditCategory", "BlogAddons", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExtractCategoryData(string categoryId)
        {
            GetCategoryDataViewModel section = await this.addonsRepository.GetCategoryById(categoryId);
            return new JsonResult(section);
        }
    }
}
