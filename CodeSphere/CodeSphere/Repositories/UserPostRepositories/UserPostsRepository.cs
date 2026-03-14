using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.UserPostRepositories
{
    public class UserPostsRepository : IUserPostsRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserPostsRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ICollection<BlogPostCardViewModel>> ExtractCreatedPostsByUsername(string username, ApplicationUser currentUser)
        {
            Expression<Func<Post, bool>> postsFilter;
            var user = await this.userManager.FindByNameAsync(username);

            if (currentUser != null &&
                (await this.userManager.IsInRoleAsync(currentUser, Roles.Administrator.ToString()) ||
                await this.userManager.IsInRoleAsync(currentUser, Roles.Editor.ToString())))
            {
                postsFilter = x => (x.PostStatus == PostStatus.Banned || x.PostStatus == PostStatus.Pending || x.PostStatus == PostStatus.Approved) && x.ApplicationUser.UserName == user.UserName;
            }
            else
            {
                if (currentUser != null)
                {
                    postsFilter = x => x.PostStatus == PostStatus.Approved && x.ApplicationUserId == user.Id;
                }
                else
                {
                    postsFilter = x => x.PostStatus == PostStatus.Approved;
                }
            }

            var posts = this.db.Posts
                .Where(postsFilter)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.FavouritePosts)
                .Include(x => x.PostLikes)
                .Include(x => x.PostsTags)
                .ThenInclude(x => x.Tag)
                .AsSplitQuery()
                .OrderByDescending(x => x.UpdatedOn)
                .ToList();

            var model = this.mapper.Map<List<BlogPostCardViewModel>>(posts);
            return model;
        }

        public async Task<ICollection<BlogPostCardViewModel>> ExtractLikedPostsByUsername(string username, ApplicationUser currentUser)
        {
            Expression<Func<Post, bool>> postsFilter;
            var user = await this.userManager.FindByNameAsync(username);

            if (currentUser != null &&
                (await this.userManager.IsInRoleAsync(currentUser, Roles.Administrator.ToString()) ||
                await this.userManager.IsInRoleAsync(currentUser, Roles.Editor.ToString())))
            {
                postsFilter = x => (x.PostStatus == PostStatus.Banned ||
                    x.PostStatus == PostStatus.Pending ||
                    x.PostStatus == PostStatus.Approved) && x.PostLikes.Any(y => y.UserId == user.Id && y.IsLiked);
            }
            else
            {
                if (currentUser != null)
                {
                    postsFilter = x => (x.PostStatus == PostStatus.Approved) && x.PostLikes.Any(y => y.UserId == user.Id && y.IsLiked);
                }
                else
                {
                    postsFilter = x => x.PostStatus == PostStatus.Approved;
                }
            }

            var posts = this.db.Posts
                .Where(postsFilter)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.FavouritePosts)
                .Include(x => x.PostLikes)
                .Include(x => x.PostsTags)
                .ThenInclude(x => x.Tag)
                .AsSplitQuery()
                .OrderByDescending(x => x.UpdatedOn)
                .ToList();

            var model = this.mapper.Map<List<BlogPostCardViewModel>>(posts);
            return model;
        }
    }
}
