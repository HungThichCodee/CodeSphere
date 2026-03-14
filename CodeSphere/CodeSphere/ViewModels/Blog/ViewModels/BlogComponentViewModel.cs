using CodeSphere.ViewModels.CategoryViewModels.ViewModels.TopCategory;
using CodeSphere.ViewModels.CommentViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.RecentPost;
using CodeSphere.ViewModels.PostViewModels.ViewModels.TopPost;
using CodeSphere.ViewModels.TagViewModels.TopTag;

namespace CodeSphere.ViewModels.Blog.ViewModels
{
    public class BlogComponentViewModel
    {
        public string Search { get; set; }

        public ICollection<RecentPostViewModel> RecentPosts { get; set; } = new HashSet<RecentPostViewModel>();

        public ICollection<TopCategoryViewModel> TopCategories { get; set; } = new HashSet<TopCategoryViewModel>();

        public ICollection<TopTagViewModel> TopTags { get; set; } = new HashSet<TopTagViewModel>();

        public ICollection<TopPostViewModel> TopPosts { get; set; } = new HashSet<TopPostViewModel>();

        public ICollection<RecentCommentViewModel> RecentComments { get; set; } = new HashSet<RecentCommentViewModel>();
    }
}
