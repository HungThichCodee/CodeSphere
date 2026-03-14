using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public class ProfileActivitiesRepository : IProfileActivitiesRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ProfileActivitiesRepository(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<ActivitiesViewModel>> ExtractActivities(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var activities = this.db.UserActions
                .Where(x => x.ApplicationUserId == user.Id)
                .Include(x => x.ApplicationUser)
                .OrderByDescending(x => x.ActionDate)
                .AsSplitQuery()
                .ToList();

            var model = this.mapper.Map<List<ActivitiesViewModel>>(activities);
            return model;
        }
    }
}
