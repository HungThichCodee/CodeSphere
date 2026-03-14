using CodeSphere.Areas.Administration.Repositories.DeleteChatStickerType;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteChatStickerType.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DeleteChatStickerTypeController : Controller
    {
        private readonly IDeleteChatStickerTypeRepository deleteChatStickerTypeRepository;

        public DeleteChatStickerTypeController(IDeleteChatStickerTypeRepository deleteChatStickerTypeRepository)
        {
            this.deleteChatStickerTypeRepository = deleteChatStickerTypeRepository;
        }

        public IActionResult Index()
        {
            var model = new DeleteChatStickerTypeBaseModel
            {
                DeleteChatStickerTypeInputModel = new DeleteChatStickerTypeInputModel(),
                DeleteChatStickerTypeViewModel = this.deleteChatStickerTypeRepository.GetAllStickersTypes(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult GetChatStickerTypeData(string stickerTypeId)
        {
            List<string> urls = this.deleteChatStickerTypeRepository.GetStickersUrls(stickerTypeId);
            return new JsonResult(new { urls = new List<string>(urls) });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChatStickerType(DeleteChatStickerTypeBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.deleteChatStickerTypeRepository
                    .DeleteChatStickerType(model.DeleteChatStickerTypeInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "DeleteChatStickerType", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "DeleteChatStickerType");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "DeleteChatStickerType", model);
            }
        }
    }
}
