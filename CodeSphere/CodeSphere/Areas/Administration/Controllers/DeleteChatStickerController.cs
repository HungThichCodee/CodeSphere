using CodeSphere.Areas.Administration.Repositories.DeleteChatSticker;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatSticker.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DeleteChatStickerController : Controller
    {
        private readonly IDeleteChatStickerRepository deleteChatStickerRepository;

        public DeleteChatStickerController(IDeleteChatStickerRepository deleteChatStickerRepository)
        {
            this.deleteChatStickerRepository = deleteChatStickerRepository;
        }

        public IActionResult Index()
        {
            var model = new DeleteChatStickerBaseModel
            {
                DeleteChatStickerInputModel = new DeleteChatStickerInputModel(),
                DeleteChatStickerViewModel = this.deleteChatStickerRepository.GetAllStickers(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatStickerData(string stickerId)
        {
            string url = await this.deleteChatStickerRepository.GetStickerUrl(stickerId);
            return new JsonResult(new { url = url });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChatSticker(DeleteChatStickerBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.deleteChatStickerRepository.DeleteChatSticker(model.DeleteChatStickerInputModel);
                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "DeleteChatSticker", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "DeleteChatSticker");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "DeleteChatSticker", model);
            }
        }
    }
}
