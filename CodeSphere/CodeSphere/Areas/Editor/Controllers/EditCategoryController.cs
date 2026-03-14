using CodeSphere.ApplicationAttributes.ActionAttributes;
using CodeSphere.Areas.Editor.Repositories.CategoryRepositories;
using CodeSphere.Areas.Editor.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Editor.Controllers
{
    [Authorize(Roles = GlobalConstants.EditorRole + "," + GlobalConstants.AdministratorRole)]
    [Area(GlobalConstants.EditorArea)]
    public class EditCategoryController : Controller
    {
        private readonly IEditCategoryRepository editCategoryRepository;

        public EditCategoryController(IEditCategoryRepository editCategoryRepository)
        {
            this.editCategoryRepository = editCategoryRepository;
        }

        [Route("Editor/EditCategory/{id?}")]
        [HttpGet]
        [UserBlocked("Index", "Profile")]
        public async Task<IActionResult> Index(string id)
        {
            EditCategoryInputModel model = await this.editCategoryRepository.ExtractCategoryById(id);
            return this.View(model);
        }

        [Route("Editor/EditCategory/{id?}")]
        [HttpPost]
        [UserBlocked("Index", "Profile")]
        public async Task<IActionResult> Index(EditCategoryInputModel model)
        {
            bool isEdited = await this.editCategoryRepository.EditCategory(model);
            if (isEdited == true)
            {
                this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyEditCategory, model.Name);
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            }

            return this.RedirectToAction("Index", "Category", new { id = model.Id });
        }
    }
}
