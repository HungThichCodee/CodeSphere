using CodeSphere.Areas.Administration.Repositories.DbUsage;
using CodeSphere.Areas.Administration.ViewModels.DbUsageViewModels.DeleteActivities;
using CodeSphere.Areas.Administration.ViewModels.DbUsageViewModels.DeleteUsersImages;
using CodeSphere.Constraints;
using CodeSphere.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DbUsageController : Controller
    {
        private readonly IDbUsageRepository dbUsageRepository;

        public DbUsageController(IDbUsageRepository dbUsageRepository)
        {
            this.dbUsageRepository = dbUsageRepository;
        }

        public IActionResult DeleteUsersActivities()
        {
            return this.View();
        }

        public IActionResult DeleteUsersImages()
        {
            var model = new DeleteUsersImagesViewModel
            {
                Usernames = this.dbUsageRepository.GetAllUsernames(),
                DeleteUserImages = new DeleteImagesByUsernameInputModel(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteActivityByName(DeleteActivitiesByNameInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                string activityText = model.ActivityName;
                string activityName = string.Join(string.Empty, activityText.Split(" "));

                UserActionType actionValue = (UserActionType)Enum.Parse(typeof(UserActionType), activityName);
                bool isRemoved = await this.dbUsageRepository.RemoveActivitiesByName(actionValue);

                if (isRemoved)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyRemoveActionByName, activityText);
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.NoActionsByGivenName, activityText);
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("DeleteUsersActivities", "DbUsage", model);
            }

            return this.RedirectToAction("DeleteUsersActivities", "DbUsage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllActivities()
        {
            int count = await this.dbUsageRepository.RemoveAllActivities();

            if (count > 0)
            {
                this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyRemoveAllActions, count);
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.NoActionsForRemoving;
            }

            return this.RedirectToAction("DeleteUsersActivities", "DbUsage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserImages(DeleteUsersImagesViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                string username = model.DeleteUserImages.Username;
                bool isDeleted = await this.dbUsageRepository.DeleteUserImagesByUsername(username);

                if (isDeleted)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyRemoveUserImages, username.ToUpper());
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.NoUserImagesByGivenUsername, username.ToUpper());
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("DeleteUsersImages", "DbUsage", model);
            }

            return this.RedirectToAction("DeleteUsersImages", "DbUsage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllusersImages()
        {
            int count = await this.dbUsageRepository.DeleteAllUsersImages();

            if (count > 0)
            {
                this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyRemoveAllUsersImages, count);
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.NoMoreUsersImagesForRemoving;
            }

            return this.RedirectToAction("DeleteUsersImages", "DbUsage");
        }
    }
}
