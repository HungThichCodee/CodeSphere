using CodeSphere.Areas.Administration.Repositories.AddEmojiWithSkin;
using CodeSphere.Areas.Administration.ViewModels.AddEmojiWithSkin.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddEmojiWithSkinController : Controller
    {
        private readonly IAddEmojiWithSkinRepository addEmojiWithSkinRepository;

        public AddEmojiWithSkinController(IAddEmojiWithSkinRepository addEmojiWithSkinRepository)
        {
            this.addEmojiWithSkinRepository = addEmojiWithSkinRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmojiWithSkin(AddEmojiWithSkinInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                string result = await this.addEmojiWithSkinRepository.AddEmojiWithSkin(model);
                this.TempData["Success"] = result;
                return this.RedirectToAction("Index", "AddEmojiWithSkin");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "AddEmojiWithSkin", model);
            }
        }
    }
}
