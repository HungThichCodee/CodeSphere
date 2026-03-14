using CodeSphere.Areas.Administration.Repositories.EditEmojiPosition;
using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.ViewModels;
using CodeSphere.Areas.PrivateChat.Models.Enums;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeSphere.Areas.Administration.Repository
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EditEmojiPositionController : Controller
    {
        private readonly IEditEmojiPositionRepository editEmojiPositionRepository;

        public EditEmojiPositionController(IEditEmojiPositionRepository editEmojiPositionRepository)
        {
            this.editEmojiPositionRepository = editEmojiPositionRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult GetEmojisPosition(EmojiType emojiType)
        {
            ICollection<EditEmojiPositionViewModel> result =
                 this.editEmojiPositionRepository.GetAllEmojisByType(emojiType);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmojisPosition(string json)
        {
            var allEmojis = JsonConvert.DeserializeObject<EditEmojiPositionInputModel[]>(json);
            var count = await this.editEmojiPositionRepository.EditEmojisPosition(allEmojis);
            this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyEditEmojisPosition, count);
            return this.Json(this.Url.Action("Index", "EditEmojiPosition"));
        }
    }
}
