using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;

namespace CodeSphere.ViewModels.TagViewModels.TagPage
{
    public class TagPageViewModel
    {
        public TagPageTagViewModel Tag { get; set; }

        public IEnumerable<BlogPostCardViewModel> Posts { get; set; } = new HashSet<BlogPostCardViewModel>();
    }
}