using CodeSphere.Areas.PrivateChat.Repositories.CollectStickers;
using CodeSphere.Areas.PrivateChat.ViewModels.CollectStickers.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Areas.PrivateChat.Controllers
{
    [Authorize]
    [Area(GlobalConstants.PrivateChatArea)]
    public class CollectStickersController : Controller
    {
        private readonly ICollectStickersRepository collectStickersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CollectStickersController(
            ICollectStickersRepository collectStickersRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.collectStickersRepository = collectStickersRepository;
            this.userManager = userManager;
        }

        [Route("PrivateChat/CollectStickers/{page?}")]
        public async Task<IActionResult> Index(int? page)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var pageNumber = page ?? 1;

            var model = new CollectStickersBaseModel
            {
                AllStickerTypes = this.collectStickersRepository
                    .GetAllStickers(currentUser)
                    .ToPagedList(pageNumber, GlobalConstants.CollectStickersOnPage),
            };

            return this.View(model);
        }

        [HttpPost]
        [Route("PrivateChat/CollectStickers/AddStickerToFavourite")]
        public async Task<IActionResult> AddStickerToFavourite(string stickerTypeId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (stickerTypeId == null || stickerTypeId == string.Empty)
            {
                return new JsonResult(new { isAdded = false });
            }

            bool result = await this.collectStickersRepository.AddStickerToFavourite(currentUser, stickerTypeId);

            return new JsonResult(new { isAdded = result });
        }

        [HttpPost]
        [Route("PrivateChat/CollectStickers/RemoveStickerFromFavourite")]
        public async Task<IActionResult> RemoveStickerFromFavourite(string stickerTypeId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (stickerTypeId == null || stickerTypeId == string.Empty)
            {
                return new JsonResult(new { isRemoved = false });
            }

            bool result = await this.collectStickersRepository
                .RemoveStickerFromFavourite(currentUser, stickerTypeId);

            return new JsonResult(new { isRemoved = result });
        }
    }
}
