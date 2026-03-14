using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;

namespace CodeSphere.ViewModels.CategoryViewModels.ViewModels.CategoryPage
{
    public class CategoryPageViewModel
    {
        public CategoryPageCategoryViewModel Category { get; set; }

        public IEnumerable<BlogPostCardViewModel> Posts { get; set; } = new HashSet<BlogPostCardViewModel>();
    }
}