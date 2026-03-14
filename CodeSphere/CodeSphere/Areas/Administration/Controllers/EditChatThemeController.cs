using CodeSphere.Areas.Administration.Repositories.EditChatTheme;
using CodeSphere.Areas.Administration.ViewModels.EditChatTheme.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatTheme.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EditChatThemeController : Controller
    {
        private readonly IEditChatThemeRepository editChatThemeRepository;

        public EditChatThemeController(IEditChatThemeRepository editChatThemeRepository)
        {
            this.editChatThemeRepository = editChatThemeRepository;
        }

        public IActionResult Index()
        {
            var model = new EditChatThemeBaseModel
            {
                EditChatThemeInput = new EditChatThemeInputModel(),
                EditChatThemeViewModels = this.editChatThemeRepository.GetAllThemes(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExtractThemeData(string themeId)
        {
            GetEditChatThemeDataViewModel section = await this.editChatThemeRepository.GetThemeById(themeId);
            return new JsonResult(section);
        }

        [HttpPost]
        public async Task<IActionResult> EditChatTheme(EditChatThemeBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.editChatThemeRepository.EditChatTheme(model.EditChatThemeInput);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "EditChatTheme", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "EditChatTheme");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "EditChatTheme", model);
            }
        }
    }
}
