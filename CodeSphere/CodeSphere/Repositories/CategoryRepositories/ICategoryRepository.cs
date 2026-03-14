using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.CategoryPage;
using CodeSphere.ViewModels.PostViewModels.ViewModels;

namespace CodeSphere.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        Task<CategoryPageCategoryViewModel> ExtractCategoryById(string id);

        Task<ICollection<BlogPostCardViewModel>> ExtractPostsByCategoryId(string id, ApplicationUser user);
    }
}
