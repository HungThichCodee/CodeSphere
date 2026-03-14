using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.TopCategory;
using CodeSphere.ViewModels.CommentViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.RecentPost;
using CodeSphere.ViewModels.PostViewModels.ViewModels.TopPost;
using CodeSphere.ViewModels.TagViewModels.TopTag;

namespace CodeSphere.Repositories.BlogRepositories
{
    public interface IBlogComponentRepository
    {
        List<TopCategoryViewModel> ExtractTopCategories();

        List<TopTagViewModel> ExtractTopTags();

        Task<List<TopPostViewModel>> ExtractTopPosts(ApplicationUser user);

        Task<List<RecentPostViewModel>> ExtractRecentPosts(ApplicationUser user);

        Task<ICollection<RecentCommentViewModel>> ExtractRecentComments(ApplicationUser currentUser);
    }
}
