using CodeSphere.Areas.Administration.Repositories.AddEmojis;
using CodeSphere.Areas.Administration.ViewModels.AddEmojis.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddEmojisController : Controller
    {
        private readonly IAddEmojisRepository addEmojisRepository;

        public AddEmojisController(IAddEmojisRepository addEmojisRepository)
        {
            this.addEmojisRepository = addEmojisRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmojis(AddEmojisInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                string mesage = await this.addEmojisRepository.AddEmojis(model);
                this.TempData["Success"] = mesage;
                return this.RedirectToAction("Index", "AddEmojis");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "AddEmojis", model);
            }
        }
    }
}
