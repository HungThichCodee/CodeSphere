using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard
{
    public class BlogPostCardViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string ImageUrl { get; set; }

        public int Likes { get; set; }

        public PostStatus PostStatus { get; set; }

        public BlogPostCardApplicationUserViewModel ApplicationUser { get; set; }

        public BlogPostCardCategoryViewModel Category { get; set; }

        public int CommentsCount { get; set; }

        public bool IsAuthor { get; set; }

        public bool IsLiked { get; set; }

        public bool IsFavourite { get; set; }

        public ICollection<BlogPostCardLikerViewModel> Likers { get; set; } = new HashSet<BlogPostCardLikerViewModel>();

        public ICollection<BlogPostCardTagViewModel> Tags { get; set; } = new HashSet<BlogPostCardTagViewModel>();
    }
}