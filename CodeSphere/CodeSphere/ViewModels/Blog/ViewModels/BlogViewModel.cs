using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;

namespace CodeSphere.ViewModels.Blog.ViewModels
{
    public class BlogViewModel
    {
        public string Search { get; set; }

        public IEnumerable<BlogPostCardViewModel> Posts { get; set; } = new HashSet<BlogPostCardViewModel>();
    }
}
