using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;

namespace CodeSphere.ViewModels.UserPostsViewModels
{
    public class UserPostsViewModel
    {
        public IEnumerable<BlogPostCardViewModel> Posts { get; set; } = new HashSet<BlogPostCardViewModel>();

        public string Action { get; set; }

        public string Username { get; set; }
    }
}
