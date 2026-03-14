using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Extensions;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.TagViewModels.TagPage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.TagRepositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public TagRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ICollection<BlogPostCardViewModel>> ExtractPostsByTagId(string id, ApplicationUser user)
        {
            Expression<Func<Post, bool>> postsFilter;

            if (user != null &&
                (await this.userManager.IsInRoleAsync(user, Roles.Administrator.ToString()) ||
                await this.userManager.IsInRoleAsync(user, Roles.Editor.ToString())))
            {
                postsFilter = x => (x.PostStatus == PostStatus.Approved ||
                    x.PostStatus == PostStatus.Banned ||
                    x.PostStatus == PostStatus.Pending) && x.PostsTags.Any(y => y.TagId == id);
            }
            else
            {
                if (user != null)
                {
                    postsFilter = x => (x.PostStatus == PostStatus.Approved ||
                        x.ApplicationUserId == user.Id) && x.PostsTags.Any(y => y.TagId == id);
                }
                else
                {
                    postsFilter = x => x.PostStatus == PostStatus.Approved && x.PostsTags.Any(y => y.TagId == id);
                }
            }

            var posts = this.db.Posts
                .Include(x => x.PostsTags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.FavouritePosts)
                .Include(x => x.PostLikes)
                .AsSplitQuery()
                .Where(postsFilter)
                .OrderByDescending(x => x.Comments.Count + x.Likes)
                .ToList();

            var model = this.mapper.Map<List<BlogPostCardViewModel>>(posts);
            return model;
        }

        public async Task<TagPageTagViewModel> ExtractTagById(string id)
        {
            var tag = await this.db.Tags.FirstOrDefaultAsync(x => x.Id == id);
            var model = this.mapper.Map<TagPageTagViewModel>(tag);
            return model;
        }
    }
}
