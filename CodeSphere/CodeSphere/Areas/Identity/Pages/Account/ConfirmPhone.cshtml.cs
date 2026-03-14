using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.Administration.Repositories.Dashboard;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeSphere.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ConfirmPhoneModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDashboardRepository dashboardRepository;
        private readonly ApplicationDbContext db;

        public ConfirmPhoneModel(
            UserManager<ApplicationUser> userManager,
            IDashboardRepository dashboardRepository,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.dashboardRepository = dashboardRepository;
            this.db = db;
        }

        public string PhoneNumber { get; set; }

        public string CountryCode { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await this.LoadPhoneNumber();

            var identityUser = await this.userManager.GetUserAsync(this.User);
            identityUser.PhoneNumberConfirmed = true;
            var updateResult = await this.userManager.UpdateAsync(identityUser);

            if (updateResult.Succeeded)
            {
                var user = this.userManager.GetUserAsync(this.HttpContext.User);
                var isAdded = await this.dashboardRepository.IsAddedUserInRole(GlobalConstants.ContributorRole, user.Result.UserName);
                if (isAdded)
                {
                    this.TempData["Success"] = string.Format(
                        SuccessMessages.SuccessfullyConfirmedPhoneNumberAndRegisteredContributorRole,
                        GlobalConstants.ContributorRole);
                }
                else
                {
                    this.TempData["Success"] = string.Format(
                        SuccessMessages.SuccessfullyConfirmedPhoneNumberInContributorRole,
                        GlobalConstants.ContributorRole);
                }

                return this.Redirect($"/Profile/{user.Result.UserName}");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "There was an error confirming the phone number, please try again");
                return this.Page();
            }
        }

        private async Task LoadPhoneNumber()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new Exception($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.PhoneNumber = user.PhoneNumber;
            this.CountryCode = this.db.CountryCodes.FirstOrDefault(x => x.Id == user.CountryCodeId).Code;
        }
    }
}
