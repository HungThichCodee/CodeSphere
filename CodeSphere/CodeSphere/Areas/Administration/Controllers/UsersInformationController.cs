using CodeSphere.Areas.Administration.Repositories.UsersInformation;
using CodeSphere.Areas.Administration.ViewModels.UsersInformation;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersInformationController : Controller
    {
        private readonly IUsersInformationRepository usersInformation;

        public UsersInformationController(IUsersInformationRepository usersInformation)
        {
            this.usersInformation = usersInformation;
        }

        public async Task<IActionResult> AllUsers()
        {
            AllUsersViewModel model = await this.usersInformation.GetAllUsers();
            return this.View(model);
        }

        public async Task<IActionResult> BannedUsers()
        {
            AllBannedUsersViewModel model = await this.usersInformation.GetAllBannedUsers();
            return this.View(model);
        }
    }
}
