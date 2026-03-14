using CodeSphere.Areas.Administration.ViewModels.UsersInformation;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.UsersInformation
{
    public class UsersInformationRepository : IUsersInformationRepository
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersInformationRepository(
            ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<AllBannedUsersViewModel> GetAllBannedUsers()
        {
            var users = this.db.Users.Where(x => x.IsBlocked == true).ToList();
            var model = new AllBannedUsersViewModel();

            foreach (var user in users)
            {
                var currentModel = new ApplicationUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RegisteredOn = user.RegisteredOn,
                    EmailConfirmed = user.EmailConfirmed,
                    IsBlocked = user.IsBlocked,
                    PhoneNumber = user.PhoneNumber,
                    //Country = await this.db.Countries.FirstOrDefaultAsync(x => x.Id == user.CountryId),
                    //City = await this.db.Cities.FirstOrDefaultAsync(x => x.Id == user.CityId),
                    //State = await this.db.States.FirstOrDefaultAsync(x => x.Id == user.StateId),
                    AboutMe = user.AboutMe,
                    //CountryCode = await this.db.CountryCodes.FirstOrDefaultAsync(x => x.Id == user.CountryCodeId),
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    //ZipCode = await this.db.ZipCodes.FirstOrDefaultAsync(x => x.Id == user.ZipCodeId),
                    ReasonToBeBlocked = user.ReasonToBeBlocked,
                };

                var userRoleNames = await this.userManager.GetRolesAsync(user);
                foreach (var roleName in userRoleNames)
                {
                    currentModel.Roles.Add(await this.db.Roles.FirstOrDefaultAsync(x => x.Name == roleName));
                }

                model.ApplicationUsers.Add(currentModel);
            }

            return model;
        }

        public async Task<AllUsersViewModel> GetAllUsers()
        {
            var users = this.db.Users.ToList();
            var model = new AllUsersViewModel();

            foreach (var user in users)
            {
                var currentModel = new ApplicationUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RegisteredOn = user.RegisteredOn,
                    EmailConfirmed = user.EmailConfirmed,
                    IsBlocked = user.IsBlocked,
                    PhoneNumber = user.PhoneNumber,
                    //Country = await this.db.Countries.FirstOrDefaultAsync(x => x.Id == user.CountryId),
                    //City = await this.db.Cities.FirstOrDefaultAsync(x => x.Id == user.CityId),
                    //State = await this.db.States.FirstOrDefaultAsync(x => x.Id == user.StateId),
                    AboutMe = user.AboutMe,
                    //CountryCode = await this.db.CountryCodes.FirstOrDefaultAsync(x => x.Id == user.CountryCodeId),
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    //ZipCode = await this.db.ZipCodes.FirstOrDefaultAsync(x => x.Id == user.ZipCodeId),
                };

                var userRoleNames = await this.userManager.GetRolesAsync(user);
                foreach (var roleName in userRoleNames)
                {
                    currentModel.Roles.Add(await this.db.Roles.FirstOrDefaultAsync(x => x.Name == roleName));
                }

                model.ApplicationUsers.Add(currentModel);
            }

            return model;
        }
    }
}
