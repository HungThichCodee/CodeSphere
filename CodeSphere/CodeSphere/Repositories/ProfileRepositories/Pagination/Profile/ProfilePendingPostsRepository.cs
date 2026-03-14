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
    public class ProfilePendingPostsRepository : IProfilePendingPostsRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ProfilePendingPostsRepository(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<PendingPostViewModel>> ExtractPendingPosts(ApplicationUser user, string currentUserId)
        {
            var currentUser = await this.userManager.FindByIdAsync(currentUserId);
            Expression<Func<PendingPost, bool>> postsFilter;

            if (currentUser.UserName == user.UserName &&
                (await this.userManager.IsInRoleAsync(currentUser, Roles.Administrator.ToString()) ||
                 await this.userManager.IsInRoleAsync(currentUser, Roles.Editor.ToString())))
            {
                postsFilter = x => x.IsPending == true;
            }
            else
            {
                postsFilter = x => x.IsPending == true && x.ApplicationUserId == user.Id;
            }

            var posts = this.db.PendingPosts
                .Where(postsFilter)
                .Include(x => x.Post)
                .ThenInclude(x => x.Category)
                .Include(x => x.ApplicationUser)
                .OrderByDescending(x => x.Post.CreatedOn)
                .AsSplitQuery()
                .ToList();

            var model = this.mapper.Map<List<PendingPostViewModel>>(posts);
            return model;
        }
    }
}
