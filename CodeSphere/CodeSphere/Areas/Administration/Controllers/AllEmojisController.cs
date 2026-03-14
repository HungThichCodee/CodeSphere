using CodeSphere.Areas.Administration.Repositories.AllEmojis;
using CodeSphere.Areas.Administration.ViewModels.AllEmojis.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AllEmojisController : Controller
    {
        private readonly IAllEmojisRepository allEmojisRepository;

        public AllEmojisController(IAllEmojisRepository allEmojisRepository)
        {
            this.allEmojisRepository = allEmojisRepository;
        }

        public IActionResult Index()
        {
            var model = new AllEmojisViewModel
            {
                AllEmojisViewModels = this.allEmojisRepository.GetAllEmojis(),
            };

            return this.View(model);
        }
    }
}
