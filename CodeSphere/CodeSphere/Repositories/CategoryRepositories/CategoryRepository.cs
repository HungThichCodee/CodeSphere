using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.CategoryPage;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public CategoryRepository(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<CategoryPageCategoryViewModel> ExtractCategoryById(string id)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            var model = this.mapper.Map<CategoryPageCategoryViewModel>(category);
            return model;
        }

        public async Task<ICollection<BlogPostCardViewModel>> ExtractPostsByCategoryId(string id, ApplicationUser user)
        {
            Expression<Func<Post, bool>> filterFunction;

            if (user != null &&
                (await this.userManager.IsInRoleAsync(user, Roles.Administrator.ToString()) ||
                await this.userManager.IsInRoleAsync(user, Roles.Editor.ToString())))
            {
                filterFunction = x => (x.PostStatus == PostStatus.Banned ||
                      x.PostStatus == PostStatus.Pending ||
                      x.PostStatus == PostStatus.Approved) && x.CategoryId == id;
            }
            else
            {
                if (user != null)
                {
                    filterFunction = x => x.PostStatus == PostStatus.Approved || x.ApplicationUserId == user.Id;
                }
                else
                {
                    filterFunction = x => x.PostStatus == PostStatus.Approved;
                }
            }

            var posts = this.db.Posts
                .Include(x => x.ApplicationUser)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.FavouritePosts)
                .Include(x => x.PostLikes)
                .AsSplitQuery()
                .Where(filterFunction)
                .OrderByDescending(x => x.UpdatedOn)
                .ToList();

            var postsModel = this.mapper.Map<List<BlogPostCardViewModel>>(posts);
            return postsModel;
        }
    }
}
