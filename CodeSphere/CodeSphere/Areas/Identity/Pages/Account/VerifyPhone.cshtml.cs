using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeSphere.Areas.Identity.Pages.Account
{
    [Authorize]
    public class VerifyPhoneModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public VerifyPhoneModel(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public string PhoneNumber { get; set; }

        public string CountryCode { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await this.LoadPhoneNumber();
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await this.LoadPhoneNumber();
            return this.RedirectToPage("ConfirmPhone");
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
