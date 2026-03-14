using CodeSphere.Areas.Administration.Repositories.AddHolidayTheme;
using CodeSphere.Areas.Administration.ViewModels.AddHolidayTheme.InputModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AddHolidayThemeController : Controller
    {
        private readonly IAddHolidayThemeRepository addHolidayThemeRepository;

        public AddHolidayThemeController(IAddHolidayThemeRepository addHolidayThemeRepository)
        {
            this.addHolidayThemeRepository = addHolidayThemeRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddHolidayThemeInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                Tuple<bool, string> result = await this.addHolidayThemeRepository.AddNewHolidayTheme(model);

                if (!result.Item1)
                {
                    this.TempData["Error"] = result.Item2;
                    return this.View();
                }

                this.TempData["Success"] = result.Item2;
                return this.RedirectToAction("Index", "AddHolidayTheme");
            }

            return this.View();
        }
    }
}
