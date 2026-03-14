using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.HomeRepositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMapper mapper;

        public HomeRepository(
            ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.mapper = mapper;
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

        public async Task<ICollection<HomeAdministratorUserViewModel>> GetAllAdministrators()
        {
            var role = await this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRole);
            var administratorsIds = this.db.UserRoles.Where(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
            var users = this.db.Users.Where(x => administratorsIds.Contains(x.Id)).ToList();
            var model = this.mapper.Map<List<HomeAdministratorUserViewModel>>(users);
            return model;
        }

        public async Task<ICollection<string>> GetHolidayThemeIcons()
        {
            var theme = await this.db.HolidayThemes
                .Include(x => x.HolidayIcons)
                .Where(x => x.IsActive)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            var result = new List<string>();

            if (theme != null)
            {
                result.AddRange(theme.HolidayIcons.Select(x => x.Url).ToList());
            }

            return result;
        }

        public ICollection<HomeLatestPostViewModel> GetLatestPosts()
        {
            var posts = this.db.Posts
                .Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .AsSplitQuery()
                .Where(x => x.PostStatus == PostStatus.Approved)
                .OrderByDescending(x => x.CreatedOn)
                .Take(GlobalConstants.LatestLayoutPostsCount)
                .ToList();

            var model = this.mapper.Map<List<HomeLatestPostViewModel>>(posts);
            return model;
        }

        public int GetPostsCount()
        {
            return this.db.Posts.Count(x => x.PostStatus == PostStatus.Approved);
        }

        public int GetRegisteredUsersCount()
        {
            return this.db.Users.Count();
        }
    }
}
