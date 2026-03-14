using CodeSphere.Areas.Administration.Repositories.AddChatStickers;
using CodeSphere.Areas.Administration.ViewModels.AddChatStickers.InputModels;
using CodeSphere.Areas.Administration.ViewModels.AddChatStickers.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddChatStickersController : Controller
    {
        private readonly IAddChatStickersRepository addChatStickersRepository;

        public AddChatStickersController(IAddChatStickersRepository addChatStickersRepository)
        {
            this.addChatStickersRepository = addChatStickersRepository;
        }

        public IActionResult Index()
        {
            var model = new AddChatStickersBaseModel
            {
                AddChatStickersInputModel = new AddChatStickersInputModel(),
                AddChatStickersViewModels = this.addChatStickersRepository.GetAllStickersTypes(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddChatStickers(AddChatStickersBaseModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result =
                    await this.addChatStickersRepository.AddChatStickers(model.AddChatStickersInputModel);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.RedirectToAction("Index", "AddChatStickers", model);
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddChatStickers");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "AddChatStickers", model);
            }
        }
    }
}
