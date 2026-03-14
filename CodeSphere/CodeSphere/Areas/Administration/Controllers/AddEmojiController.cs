using CodeSphere.Areas.Administration.Repositories.AddEmoji;
using CodeSphere.Areas.Administration.ViewModels.AddEmoji.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddEmojiController : Controller
    {
        private readonly IAddEmojiRepository addEmojiRepository;

        public AddEmojiController(IAddEmojiRepository addEmojiRepository)
        {
            this.addEmojiRepository = addEmojiRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmoji(AddEmojiInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.addEmojiRepository.AddEmoji(model);
                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "AddEmoji", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddEmoji");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "AddEmoji", model);
            }
        }
    }
}
