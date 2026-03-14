using CodeSphere.Areas.Administration.Repositories.Dashboard;
using CodeSphere.Areas.Administration.ViewModels.DashboardViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            DashboardViewModel dashboard = this.dashboardRepository.GetDashboardInformation();
            DashboardIndexViewModel model = new DashboardIndexViewModel
            {
                DashboardViewModel = dashboard,
                CreateRole = new CreateRoleInputModel(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(DashboardIndexViewModel model)
        {
            string role = model.CreateRole.Role;

            if (this.ModelState.IsValid)
            {
                IdentityResult result = await this.dashboardRepository.CreateRole(role);

                if (result.Succeeded)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyAddedRole, role);
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.RoleExist, role);
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "Dashboard", model);
            }

            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserInRole(DashboardIndexViewModel model)
        {
            string inputRole = model.AddUserInRole.Role;
            string inputUsername = model.AddUserInRole.Username;

            if (this.ModelState.IsValid)
            {
                var isAdded = await this.dashboardRepository.IsAddedUserInRole(inputRole, inputUsername);

                if (isAdded)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyAddedUserInRole, inputUsername.ToUpper(), inputRole);
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.UserAlreadyInRole, inputUsername.ToUpper(), inputRole);
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "Dashboard", model);
            }

            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(DashboardIndexViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var username = model.RemoveUserFromRole.Username;
                var role = model.RemoveUserFromRole.Role;
                bool isRemoved = await this.dashboardRepository.RemoveUserFromRole(username, role);

                if (isRemoved)
                {
                    this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyRemoveUserRole, username.ToUpper(), role);
                    return this.Redirect($"/Profile/{username}");
                }
                else
                {
                    this.TempData["Error"] = string.Format(ErrorMessages.UserNotInRole, username.ToUpper(), role);
                }
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "Dashboard", model);
            }

            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> SyncFollowUnfollow()
        {
            bool isSync = await this.dashboardRepository.SyncFollowUnfollow();

            if (isSync)
            {
                this.TempData["Success"] = SuccessMessages.SuccessfullySyncFollowUnfollow;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.NoDataForSyncFollowUnfollow;
            }

            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}