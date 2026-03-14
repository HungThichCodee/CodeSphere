using CodeSphere.Areas.Administration.Repositories.AddChatTheme;
using CodeSphere.Areas.Administration.ViewModels.AddChatTheme.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddChatThemeController : Controller
    {
        private readonly IAddChatThemeRepository addChatThemeRepository;

        public AddChatThemeController(IAddChatThemeRepository addChatThemeRepository)
        {
            this.addChatThemeRepository = addChatThemeRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddChatTheme(AddChatThemeInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result = await this.addChatThemeRepository.AddChatTheme(model);
                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "AddChatTheme", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddChatTheme");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "AddChatTheme", model);
            }
        }
    }
}
