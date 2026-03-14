using CodeSphere.Data;
using CodeSphere.Extensions;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;

namespace CodeSphere.Services
{
    public class AdminAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminAccountService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureAdminAccountExistsAsync()
        {

            var adminRole = await this._roleManager.FindByNameAsync("Administrator");
            if (adminRole == null)
            {
                await this._roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });
            }

            var users = await this._dbContext.Users.ToListAsync();
            var adminExists = false;

            foreach (var user in users)
            {
                if (await this._userManager.IsInRoleAsync(user, "Administrator"))
                {
                    adminExists = true;
                    break;
                }
            }

            if (!adminExists)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "dungtrantrung603@gmail.com",
                    EmailConfirmed = true,
                    RegisteredOn = DateTime.UtcNow
                };

                var result = await this._userManager.CreateAsync(adminUser, "Admin@123"); 
                if (result.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }

        public async Task EnsureEditorAccountsExistAsync()
        {
            var editorRole = await this._roleManager.FindByNameAsync("Editor");
            if (editorRole == null)
            {
                await this._roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Editor",
                    NormalizedName = "EDITOR"
                });
            }

            var editors = new[]
            {
                new { UserName = "DungEditor1", Email = "editor1@codesphere.local", Password = "Editor@123" },
                new { UserName = "LamHungEditor2", Email = "editor2@codesphere.local", Password = "Editor@123" },
                new { UserName = "ThaiHungEditor3", Email = "editor3@codesphere.local", Password = "Editor@123" },
            };

            foreach (var e in editors)
            {
                var existingByName = await this._userManager.FindByNameAsync(e.UserName);
                if (existingByName != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByName, "Editor"))
                    {
                        await this._userManager.AddToRoleAsync(existingByName, "Editor");
                    }

                    continue;
                }

                var existingByEmail = await this._userManager.FindByEmailAsync(e.Email);
                if (existingByEmail != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByEmail, "Editor"))
                    {
                        await this._userManager.AddToRoleAsync(existingByEmail, "Editor");
                    }

                    continue;
                }

                var user = new ApplicationUser
                {
                    UserName = e.UserName,
                    Email = e.Email,
                    EmailConfirmed = true,
                    RegisteredOn = DateTime.UtcNow
                };

                var createResult = await this._userManager.CreateAsync(user, e.Password);
                if (createResult.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, "Editor");
                }
                else
                {
                    var fallbackPassword = "Editor@123";
                    var fallbackResult = await this._userManager.CreateAsync(user, fallbackPassword);
                    if (fallbackResult.Succeeded)
                    {
                        await this._userManager.AddToRoleAsync(user, "Editor");
                    }
                }
            }
        }

        public async Task EnsureSubscriberAccountsExistAsync()
        {
            var subscriberRole = await this._roleManager.FindByNameAsync("Subscriber");
            if (subscriberRole == null)
            {
                await this._roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Subscriber",
                    NormalizedName = "SUBSCRIBER"
                });
            }

            var subscribers = new[]
            {
                new { UserName = "ThaiHungSub", Email = "sub1@codesphere.local", Password = "Dung@123" },
                new { UserName = "lamHungSub", Email = "sub2@codesphere.local", Password = "Dung@123" },
                new { UserName = "DungSub", Email = "sub3@codesphere.local", Password = "Dung@123" }
            };

            foreach (var s in subscribers)
            {
                var existingByName = await this._userManager.FindByNameAsync(s.UserName);
                if (existingByName != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByName, "Subscriber"))
                    {
                        await this._userManager.AddToRoleAsync(existingByName, "Subscriber");
                    }

                    continue;
                }

                var existingByEmail = await this._userManager.FindByEmailAsync(s.Email);
                if (existingByEmail != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByEmail, "Subscriber"))
                    {
                        await this._userManager.AddToRoleAsync(existingByEmail, "Subscriber");
                    }

                    continue;
                }

                var user = new ApplicationUser
                {
                    UserName = s.UserName,
                    Email = s.Email,
                    EmailConfirmed = true,
                    RegisteredOn = DateTime.UtcNow
                };

                var createResult = await this._userManager.CreateAsync(user, s.Password);
                if (createResult.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, "Subscriber");
                }
            }
        }

        public async Task EnsureContributorAccountsExistAsync()
        {
            var contributorRole = await this._roleManager.FindByNameAsync("Contributor");
            if (contributorRole == null)
            {
                await this._roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Contributor",
                    NormalizedName = "CONTRIBUTOR"
                });
            }

            var contributors = new[]
            {
                new { UserName = "DungUser", Email = "contrib1@codesphere.local", Password = "Dung@123", Phone = "+10000000001" },
                new { UserName = "LamHungUser", Email = "contrib2@codesphere.local", Password = "Dung@123", Phone = "+10000000002" },
                new { UserName = "ThaiHungUser", Email = "contrib3@codesphere.local", Password = "Dung@123", Phone = "+10000000003" },
                new { UserName = "DanUser", Email = "contrib4@codesphere.local", Password = "Dung@123", Phone = "+10000000004" },
                new { UserName = "TrinhUser", Email = "contrib5@codesphere.local", Password = "Dung@123", Phone = "+10000000005" }
            };

            foreach (var c in contributors)
            {
                var existingByName = await this._userManager.FindByNameAsync(c.UserName);
                if (existingByName != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByName, "Contributor"))
                    {
                        await this._userManager.AddToRoleAsync(existingByName, "Contributor");
                    }

                    // ensure phone confirmed if phone exists
                    if (!string.IsNullOrEmpty(existingByName.PhoneNumber) && !existingByName.PhoneNumberConfirmed)
                    {
                        existingByName.PhoneNumberConfirmed = true;
                        await this._userManager.UpdateAsync(existingByName);
                    }

                    continue;
                }

                var existingByEmail = await this._userManager.FindByEmailAsync(c.Email);
                if (existingByEmail != null)
                {
                    if (!await this._userManager.IsInRoleAsync(existingByEmail, "Contributor"))
                    {
                        await this._userManager.AddToRoleAsync(existingByEmail, "Contributor");
                    }

                    if (!string.IsNullOrEmpty(existingByEmail.PhoneNumber) && !existingByEmail.PhoneNumberConfirmed)
                    {
                        existingByEmail.PhoneNumberConfirmed = true;
                        await this._userManager.UpdateAsync(existingByEmail);
                    }

                    continue;
                }

                var user = new ApplicationUser
                {
                    UserName = c.UserName,
                    Email = c.Email,
                    PhoneNumber = c.Phone,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    RegisteredOn = DateTime.UtcNow
                };

                var createResult = await this._userManager.CreateAsync(user, c.Password);
                if (createResult.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, "Contributor");
                }
            }
        }
    }
}