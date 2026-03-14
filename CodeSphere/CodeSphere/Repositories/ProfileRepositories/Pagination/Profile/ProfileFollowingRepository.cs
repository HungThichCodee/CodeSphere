using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public class ProfileFollowingRepository : IProfileFollowingRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProfileFollowingRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<FollowingViewModel>> ExtractFollowing(string username)
        {
            var followers = await this.db.FollowUnfollows
                .Where(x => x.Follower.UserName == username && x.IsFollowed == true)
                .Include(x => x.ApplicationUser)
                .Select(x => x.ApplicationUser)
                .AsSplitQuery()
                .ToListAsync();

            var model = this.mapper.Map<List<FollowingViewModel>>(followers);
            return model;
        }
    }
}
