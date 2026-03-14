// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Users.InputModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CodeSphere.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationDbContext db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleManager = roleManager;
            this.db = db;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Username,
                    Email = this.Input.Email,
                    RegisteredOn = DateTime.UtcNow,
                    ImageUrl = GlobalConstants.NoAvatarImageLocation,
                    CoverImageUrl = GlobalConstants.NoCoverImageLocation,
                    EmailConfirmed = true,
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    ApplicationRole role = await this.roleManager.FindByNameAsync(GlobalConstants.SubscriberRole);

                    if (role == null)
                    {
                        IdentityResult resultRole = await this.CreateRole(GlobalConstants.SubscriberRole);

                        if (resultRole.Succeeded)
                        {
                            role = await this.roleManager.FindByNameAsync(GlobalConstants.SubscriberRole);
                        }
                    }

                    var isExist = this.db.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

                    if (!isExist)
                    {
                        this.db.UserRoles.Add(new ApplicationUserRole()
                        {
                            RoleId = role.Id,
                            UserId = user.Id,
                        });

                        await this.db.SaveChangesAsync();
                    }

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public async Task<IdentityResult> CreateRole(string role)
        {
            Roles roleValue = (Roles)Enum.Parse(typeof(Roles), role);
            ApplicationRole identityRole = new ApplicationRole
            {
                Name = role,
                RoleLevel = (int)roleValue,
            };

            IdentityResult result = await this.roleManager.CreateAsync(identityRole);
            return result;
        }
    }
}
