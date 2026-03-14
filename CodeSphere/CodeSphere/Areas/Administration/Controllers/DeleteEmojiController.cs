using CodeSphere.Areas.Administration.Repositories.DeleteEmoji;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DeleteEmojiController : Controller
    {
        private readonly IDeleteEmojiRepository deleteEmojiRepository;

        public DeleteEmojiController(IDeleteEmojiRepository deleteEmojiRepository)
        {
            this.deleteEmojiRepository = deleteEmojiRepository;
        }

        public IActionResult Index()
        {
            var model = new DeleteEmojiBaseModel
            {
                DeleteEmojiInputModel = new DeleteEmojiInputModel(),
                DeleteEmojiViewModels = this.deleteEmojiRepository.GetAllEmojis(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmojiData(string emojiId)
        {
            string url = await this.deleteEmojiRepository.GetEmojiUrl(emojiId);
            return new JsonResult(new { url = url });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmoji(DeleteEmojiBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.deleteEmojiRepository.DeleteEmoji(model.DeleteEmojiInputModel);
                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "DeleteEmoji", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "DeleteEmoji");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "DeleteEmoji", model);
            }
        }
    }
}
