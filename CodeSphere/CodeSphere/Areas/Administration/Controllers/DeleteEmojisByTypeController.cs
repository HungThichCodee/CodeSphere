using CodeSphere.Areas.Administration.Repositories.DeleteEmojisByType;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmojisByType.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DeleteEmojisByTypeController : Controller
    {
        private readonly IDeleteEmojisByTypeRepository deleteEmojisByTypeRepository;

        public DeleteEmojisByTypeController(IDeleteEmojisByTypeRepository deleteEmojisByTypeRepository)
        {
            this.deleteEmojisByTypeRepository = deleteEmojisByTypeRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmojisByType(DeleteEmojisByTypeInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.TempData["Success"] =
                    await this.deleteEmojisByTypeRepository.DeleteEmojisByType(model.EmojiType);
                return this.RedirectToAction("Index", "DeleteEmojisByType", model);
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "DeleteEmojisByType", model);
            }
        }
    }
}
