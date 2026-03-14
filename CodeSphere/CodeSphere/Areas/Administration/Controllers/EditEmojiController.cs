using CodeSphere.Areas.Administration.Repositories.EditEmoji;
using CodeSphere.Areas.Administration.ViewModels.EditEmoji.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditEmoji.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EditEmojiController : Controller
    {
        private readonly IEditEmojiRepository editEmojiRepository;

        public EditEmojiController(IEditEmojiRepository editEmojiRepository)
        {
            this.editEmojiRepository = editEmojiRepository;
        }

        public IActionResult Index()
        {
            var model = new EditEmojiBaseModel
            {
                EditEmojiInputModel = new EditEmojiInputModel(),
                EditEmojiViewModel = this.editEmojiRepository.GetAllEmojis(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmojiData(string emojiId)
        {
            GetEditEmojiDataViewModel section = await this.editEmojiRepository.GetEmojiById(emojiId);
            return new JsonResult(section);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmoji(EditEmojiBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result = await this.editEmojiRepository.EditEmoji(model.EditEmojiInputModel);
                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "EditEmoji", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "EditEmoji");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "EditEmoji", model);
            }
        }
    }
}
