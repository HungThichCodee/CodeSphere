using CodeSphere.Models.User;
using CodeSphere.Repositories.BlogRepositories;
using CodeSphere.ViewModels.Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogComponentRepository blogComponentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogViewComponent(IBlogComponentRepository blogComponentRepository, UserManager<ApplicationUser> userManager)
        {
            this.blogComponentRepository = blogComponentRepository;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string search)
        {
            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
            BlogComponentViewModel components = new BlogComponentViewModel
            {
                RecentPosts = await this.blogComponentRepository.ExtractRecentPosts(currentUser),
                TopCategories = this.blogComponentRepository.ExtractTopCategories(),
                TopPosts = await this.blogComponentRepository.ExtractTopPosts(currentUser),
                TopTags = this.blogComponentRepository.ExtractTopTags(),
                RecentComments = await this.blogComponentRepository.ExtractRecentComments(currentUser),
                Search = search,
            };

            return this.View(components);
        }
    }
}