using CodeSphere.Areas.Administration.Repositories.EditChatSticker;
using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatSticker.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EditChatStickerController : Controller
    {
        private readonly IEditChatStickerRepository editChatStickerRepository;

        public EditChatStickerController(IEditChatStickerRepository editChatStickerRepository)
        {
            this.editChatStickerRepository = editChatStickerRepository;
        }

        public IActionResult Index()
        {
            var model = new EditChatStickerBaseModel
            {
                EditChatStickerInputModel = new EditChatStickerInputModel(),
                AllStikersTypes = this.editChatStickerRepository.GetAllStikersTypes(),
                EditChatStickerViewModels = this.editChatStickerRepository.GetAllStickers(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStickerData(string stickerId)
        {
            GetEditChatStickerDataViewModel section =
                await this.editChatStickerRepository.GetStickerById(stickerId);
            return new JsonResult(section);
        }

        [HttpPost]
        public async Task<IActionResult> EditSticker(EditChatStickerBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.editChatStickerRepository.EditSticker(model.EditChatStickerInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "EditChatSticker", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "EditChatSticker");
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "EditChatSticker", model);
        }
    }
}
