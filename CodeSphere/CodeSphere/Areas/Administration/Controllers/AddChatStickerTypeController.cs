using CodeSphere.Areas.Administration.Repositories.AddChatStickerType;
using CodeSphere.Areas.Administration.ViewModels.AddChatStickerType.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddChatStickerTypeController : Controller
    {
        private readonly IAddChatStickerTypeRepository addChatStickerTypeRepository;

        public AddChatStickerTypeController(IAddChatStickerTypeRepository addChatStickerTypeRepository)
        {
            this.addChatStickerTypeRepository = addChatStickerTypeRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStickerType(AddChatStickerTypeInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result = await this.addChatStickerTypeRepository.AddNewStickerType(model);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "AddChatStickerType", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddChatStickerType");
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "AddChatStickerType", model);
        }
    }
}
