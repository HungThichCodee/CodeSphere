using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;

namespace CodeSphere.Repositories.UserPostRepositories
{
    public interface IUserPostsRepository
    {
        Task<ICollection<BlogPostCardViewModel>> ExtractLikedPostsByUsername(string username, ApplicationUser user);

        Task<ICollection<BlogPostCardViewModel>> ExtractCreatedPostsByUsername(string username, ApplicationUser user);
    }
}
