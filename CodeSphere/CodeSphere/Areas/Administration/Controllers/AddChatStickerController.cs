using CodeSphere.Areas.Administration.Repositories.AddChatSticker;
using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.InputModels;
using CodeSphere.Areas.Administration.ViewModels.AddChatSticker.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddChatStickerController : Controller
    {
        private readonly IAddChatStickerRepository addChatStickerRepository;

        public AddChatStickerController(IAddChatStickerRepository addChatStickerRepository)
        {
            this.addChatStickerRepository = addChatStickerRepository;
        }

        public IActionResult Index()
        {
            var model = new AddChatStickerBaseModel
            {
                AddChatStickerInputModel = new AddChatStickerInputModel(),
                AddChatStickerViewModels = this.addChatStickerRepository.GetAllStickerTypes(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSticker(AddChatStickerBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.addChatStickerRepository.AddNewSticker(model.AddChatStickerInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "AddChatSticker", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddChatSticker");
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "AddChatSticker", model);
        }
    }
}
