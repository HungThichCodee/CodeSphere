using CodeSphere.Areas.Administration.Repositories.AllChatStickers;
using CodeSphere.Areas.Administration.ViewModels.AllChatStickers.ViewModels;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AllChatStickersController : Controller
    {
        private readonly IAllChatStickersRepository allChatStickersRepository;

        public AllChatStickersController(IAllChatStickersRepository allChatStickersRepository)
        {
            this.allChatStickersRepository = allChatStickersRepository;
        }

        public IActionResult Index()
        {
            var model = new List<AllChatStickersViewModel>(this.allChatStickersRepository.GetAllChatStickers());

            return this.View(model);
        }
    }
}
