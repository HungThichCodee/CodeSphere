using CodeSphere.Areas.Administration.Repositories.EditChatStickerType;
using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditChatStickerType.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EditChatStickerTypeController : Controller
    {
        private readonly IEditChatStickerTypeRepository editChatStickerTypeRepository;

        public EditChatStickerTypeController(IEditChatStickerTypeRepository editChatStickerTypeRepository)
        {
            this.editChatStickerTypeRepository = editChatStickerTypeRepository;
        }

        public IActionResult Index()
        {
            var model = new EditChatStickerTypeBaseModel
            {
                EditChatStickerTypeInputModel = new EditChatStickerTypeInputModel(),
                EditChatStickerTypeViewModels = this.editChatStickerTypeRepository.GetAllChatStickerTypes(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStickerTypeData(string stickerTypeId)
        {
            GetEditChatStickerTypeDataViewModel section =
                await this.editChatStickerTypeRepository.GetStickerTypeById(stickerTypeId);
            return new JsonResult(section);
        }

        [HttpPost]
        public async Task<IActionResult> EditStickerType(EditChatStickerTypeBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.editChatStickerTypeRepository.EditStickerType(model.EditChatStickerTypeInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "EditChatStickerType", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "EditChatStickerType");
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "EditChatStickerType", model);
        }
    }
}
