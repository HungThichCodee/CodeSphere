using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.Repositories.ProfileRepositories;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IProfileRepository profileRepository;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IProfileRepository profileRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.profileRepository = profileRepository;
        }

        [HttpGet]
        [Route("Profile/{username}/{tab?}/{page?}")]
        public async Task<IActionResult> Index(string username, ProfileTab tab, int? page)
        {
            if (!await this.profileRepository.IsUserExist(username))
            {
                return this.NotFound();
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            var user = await this.profileRepository.ExtractUserInfo(username, currentUser);

            var adminRole = await this.roleManager.FindByNameAsync(Roles.Administrator.ToString());
            bool hasAdmin = await this.profileRepository.HasAdmin(adminRole);

            var pageNumber = page ?? 1;

            var model = new ProfileViewModel
            {
                ApplicationUser = user,
                HasAdmin = hasAdmin,
                RatingScore = this.profileRepository.ExtractUserRatingScore(username),
                LatestScore = await this.profileRepository.GetLatestScore(currentUser, username),
                ActiveTab = tab,
                Page = pageNumber,
            };

            return this.View(model);
        }

        [HttpGet]
        [Route("/Profile/SwitchToAllActivitiesTabs/{username}/{tab}/{page}")]
        public async Task<IActionResult> SwitchToAllActivitiesTabs(string username, string tab, int page)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var tabEnum = tab switch
            {
                "Activities" => ProfileTab.Activities,
                "Following" => ProfileTab.Following,
                "Followers" => ProfileTab.Followers,
                "Favorites" => ProfileTab.Favorites,
                "PendingPosts" => ProfileTab.PendingPosts,
                "BannedPosts" => ProfileTab.BannedPosts,
                _ => ProfileTab.Activities,
            };

            return this.RedirectToAction("Index", new { username = user.UserName, tab = tabEnum, page });
        }

        [HttpGet]
        [Route("/Profile/SwitchToAllUsersTabs/{tab}/{page}")]
        public IActionResult SwitchToAllUsersTabs(string tab, int page)
        {
            var tabEnum = tab switch
            {
                "AllUsers" => AllUsersTab.AllUsers,
                "RecommendedUsers" => AllUsersTab.RecommendedUsers,
                "BannedUsers" => AllUsersTab.BannedUsers,
                "AllAdministrators" => AllUsersTab.AllAdministrators,
                _ => AllUsersTab.AllUsers,
            };

            return this.RedirectToAction("Users", new { tab = tabEnum, page });
        }

        [HttpPost]
        [Route("/Follow/{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            ApplicationUser user = await this.profileRepository.FollowUser(username, currentUser);
            this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyFollowedUser, username.ToUpper());

            return this.Redirect($"/Profile/{user.UserName}");
        }

        [HttpPost]
        [Route("/Unfollow/{username}")]
        public async Task<IActionResult> Unfollow(string username)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            ApplicationUser user = await this.profileRepository.UnfollowUser(username, currentUser);
            this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyUnfollowedUser, username.ToUpper());

            return this.Redirect($"/Profile/{user.UserName}");
        }

        [HttpGet]
        [Route("/Profile/Users/{tab?}/{page?}/{search?}")]
        public IActionResult Users(AllUsersTab tab, int? page, string search)
        {
            var pageNumber = page ?? 1;

            if (search != null)
            {
                pageNumber = 1;
            }

            var model = new UsersViewModel
            {
                Search = search,
                ActiveTab = tab,
                Page = pageNumber,
            };

            return this.View(model);
        }

        [HttpPost]
        [Route("/RateUser")]
        public async Task<string> RateUser(string username, int rate)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            double rateUser = await this.profileRepository.RateUser(currentUser, username, rate);
            return $"{rateUser:F2}/5";
        }

        [HttpPost]
        [Route("/DeleteActivityHistory/{username}")]
        public async Task<IActionResult> DeleteActivityHistory(string username)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            await this.profileRepository.DeleteActivity(currentUser);
            this.TempData["Success"] = SuccessMessages.SuccessfullyDeleteAllActivity;

            return this.Redirect($"/Profile/{username}");
        }

        [HttpPost]
        [Route("/DeleteActivityById/{username}/{activityId}")]
        public async Task<IActionResult> DeleteActivityById(string username, string activityId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            string activityName = await this.profileRepository.DeleteActivityById(currentUser, activityId);
            this.TempData["Success"] = string.Format(SuccessMessages.SuccessfullyDeletedActivityById, activityName);

            return this.Redirect($"/Profile/{username}");
        }

        [HttpPost]
        [Route("/Profile/{username}/changeActionStatus")]
        public async Task<string> ChangeActionStatus(string username, string id, string newStatus)
        {
            await this.profileRepository.ChangeActionStatus(username, id, newStatus);
            return newStatus;
        }

        [HttpPost]
        public async Task<IActionResult> MakeYourselfAdmin(string username)
        {
            var hasAdmin = await this.profileRepository.HasAdministrator();

            if (hasAdmin)
            {
                return this.BadRequest();
            }

            this.profileRepository.MakeYourselfAdmin(username);
            return this.Redirect($"/Profile/{username}");
        }

        [HttpGet]
        [Route("/Profile/LoadTabContent/{username}/{tab}/{page}")]
        public async Task<IActionResult> LoadTabContent(string username, string tab, int page)
        {
            if (!await this.profileRepository.IsUserExist(username))
            {
                return this.NotFound();
            }

            var tabEnum = tab switch
            {
                "Activities" => ProfileTab.Activities,
                "Following" => ProfileTab.Following,
                "Followers" => ProfileTab.Followers,
                "Favorites" => ProfileTab.Favorites,
                "PendingPosts" => ProfileTab.PendingPosts,
                "BannedPosts" => ProfileTab.BannedPosts,
                _ => ProfileTab.Activities,
            };

            switch (tabEnum)
            {
                case ProfileTab.Activities:
                    return ViewComponent("Activities", new { username, page });
                case ProfileTab.Following:
                    return ViewComponent("Following", new { username, page });
                case ProfileTab.Followers:
                    return ViewComponent("Followers", new { username, page });
                case ProfileTab.Favorites:
                    return ViewComponent("Favorites", new { username, page });
                case ProfileTab.PendingPosts:
                    return ViewComponent("PendingPosts", new { username, page });
                case ProfileTab.BannedPosts:
                    return ViewComponent("BannedPosts", new { username, page });
                default:
                    return ViewComponent("Activities", new { username, page });
            }
        }
    }
}
