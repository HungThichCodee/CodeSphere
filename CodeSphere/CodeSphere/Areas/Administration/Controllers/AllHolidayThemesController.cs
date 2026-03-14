using CodeSphere.Areas.Administration.Repositories.AllHolidayThemes;
using CodeSphere.Areas.Administration.ViewModels.AllHolidayThemes.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AllHolidayThemesController : Controller
    {
        private readonly IAllHolidayThemesRepository allHolidayThemesRepository;

        public AllHolidayThemesController(IAllHolidayThemesRepository allHolidayThemesRepository)
        {
            this.allHolidayThemesRepository = allHolidayThemesRepository;
        }

        public IActionResult Index()
        {
            ICollection<AllHolidayThemesViewModel> model = this.allHolidayThemesRepository.GetAllHolidayThemes();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeThemeStatus(string id, bool status)
        {
            Tuple<bool, string> result = await this.allHolidayThemesRepository.ChangeHolidayThemeStatus(id, status);
            if (!result.Item1)
            {
                this.TempData["Error"] = result.Item2;
            }
            else
            {
                this.TempData["Success"] = result.Item2;
            }

            return this.RedirectToAction("Index", "AllHolidayThemes");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHolidayTheme(string id)
        {
            Tuple<bool, string> result = await this.allHolidayThemesRepository.DeleteHolidayTheme(id);
            if (!result.Item1)
            {
                this.TempData["Error"] = result.Item2;
            }
            else
            {
                this.TempData["Success"] = result.Item2;
            }

            return this.RedirectToAction("Index", "AllHolidayThemes");
        }
    }
}
