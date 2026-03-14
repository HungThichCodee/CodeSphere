using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public class ProfileBannedPostsRepository : IProfileBannedPostsRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ProfileBannedPostsRepository(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<BannedPostViewModel>> ExtractBannedPosts(ApplicationUser user, string currentUserId)
        {
            var currentUser = await this.userManager.FindByIdAsync(currentUserId);
            Expression<Func<BlockedPost, bool>> postsFilter;

            if (currentUser.UserName == user.UserName &&
                (await this.userManager.IsInRoleAsync(currentUser, Roles.Administrator.ToString()) ||
                 await this.userManager.IsInRoleAsync(currentUser, Roles.Editor.ToString())))
            {
                postsFilter = x => x.IsBlocked == true;
            }
            else
            {
                postsFilter = x => x.IsBlocked == true && x.ApplicationUserId == user.Id;
            }

            var posts = this.db.BlockedPosts
                .Include(x => x.Post)
                .ThenInclude(x => x.Category)
                .Include(x => x.ApplicationUser)
                .Where(postsFilter)
                .AsSplitQuery()
                .ToList();

            var model = this.mapper.Map<List<BannedPostViewModel>>(posts);
            return model;
        }
    }
}
