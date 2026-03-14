using CodeSphere.Areas.Administration.Repositories.UserPenalties;
using CodeSphere.Areas.Administration.ViewModels.UsersPenalties;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersPenaltiesController : Controller
    {
        private readonly IUsersPenaltiesRepository usersPenaltiesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersPenaltiesController(
            IUsersPenaltiesRepository usersPenaltiesRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.usersPenaltiesRepository = usersPenaltiesRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> BlockUnblockUser()
        {
            var model = new UsersPenaltiesIndexModel
            {
                UsersPenaltiesViewModel = new UsersPenaltiesViewModel
                {
                    BlockedUsernames = this.usersPenaltiesRepository.GetAllBlockedUsers(),
                    NotBlockedUsernames = await this.usersPenaltiesRepository.GetAllNotBlockedUsers(),
                },
                UsersPenaltiesInputModel = new UsersPenaltiesInputModel(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser(UsersPenaltiesIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                string username = model.UsersPenaltiesInputModel.Username;
                var currentUser = await this.userManager.GetUserAsync(this.User);
                bool isBlocked = await this.usersPenaltiesRepository.BlockUser(username, currentUser, model.UsersPenaltiesInputModel.ReasonToBeBlocked);

                if (isBlocked)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyBlockedUser, username.ToUpper());
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.UserAlreadyBlocked, username.ToUpper());
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("BlockUnblockUser", "UsersPenalties", model);
            }

            return this.RedirectToAction("BlockUnblockUser", "UsersPenalties");
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUser(UsersPenaltiesIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                string username = model.UsersPenaltiesInputModel.Username;
                var currentUser = await this.userManager.GetUserAsync(this.User);
                bool isUnblocked = await this.usersPenaltiesRepository.UnblockUser(username, currentUser);

                if (isUnblocked)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyUnblockedUser, username.ToUpper());
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.UserAlreadyUnblocked, username.ToUpper());
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("BlockUnblockUser", "UsersPenalties", model);
            }

            return this.RedirectToAction("BlockUnblockUser", "UsersPenalties");
        }

        [HttpPost]
        public async Task<IActionResult> BlockAllUsers()
        {
            int count = await this.usersPenaltiesRepository.BlockAllUsers();

            if (count > 0)
            {
                this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyBlockedAllUsers, count);
            }
            else
            {
                this.TempData["Error"] = string.Format(ErrorMessages.AllUsersAlreadyBlocked);
            }

            return this.RedirectToAction("BlockUnblockUser", "UsersPenalties");
        }

        [HttpPost]
        public async Task<IActionResult> UnblockAllUsers()
        {
            int count = await this.usersPenaltiesRepository.UnblockAllUsers();

            if (count > 0)
            {
                this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyUnblockedAllUsers, count);
            }
            else
            {
                this.TempData["Error"] = string.Format(ErrorMessages.AllUsersAlreadyUnblocked);
            }

            return this.RedirectToAction("BlockUnblockUser", "UsersPenalties");
        }

        public IActionResult HangFire()
        {
            return this.RedirectToPage("/Administration/UsersPenalties/HangFire");
        }
    }
}
