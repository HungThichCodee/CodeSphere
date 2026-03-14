using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public class ProfileFollowersRepository : IProfileFollowersRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProfileFollowersRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<FollowersViewModel>> ExtractFollowers(string username)
        {
            var followers = await this.db.FollowUnfollows
                .Where(x => x.ApplicationUser.UserName == username && x.IsFollowed == true)
                .Include(x => x.Follower)
                .Select(x => x.Follower)
                .AsSplitQuery()
                .ToListAsync();

            var model = this.mapper.Map<List<FollowersViewModel>>(followers);
            return model;
        }
    }
}
