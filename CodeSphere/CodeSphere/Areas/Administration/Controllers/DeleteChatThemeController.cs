using CodeSphere.Areas.Administration.Repositories.DeleteChatTheme;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatTheme.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DeleteChatThemeController : Controller
    {
        private readonly IDeleteChatThemeRepository removeChatThemeRepository;

        public DeleteChatThemeController(IDeleteChatThemeRepository removeChatThemeRepository)
        {
            this.removeChatThemeRepository = removeChatThemeRepository;
        }

        public IActionResult Index()
        {
            var model = new DeleteChatThemeBaseViewModel
            {
                DeleteChatThemeInputModel = new DeleteChatThemeInputModel(),
                DeleteChatThemeViewModels = this.removeChatThemeRepository.GetAllChatThemes(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExtractThemeData(string themeId)
        {
            GetDeleteChatThemeDataViewModel section = await this.removeChatThemeRepository.GetThemeById(themeId);
            return new JsonResult(section);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChatTheme(DeleteChatThemeBaseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.removeChatThemeRepository.DeleteChatTheme(model.DeleteChatThemeInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "DeleteChatTheme", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "DeleteChatTheme");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "DeleteChatTheme", model);
            }
        }
    }
}
