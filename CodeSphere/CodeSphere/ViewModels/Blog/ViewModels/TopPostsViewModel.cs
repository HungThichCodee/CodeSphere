using CodeSphere.Models.Enums;
using CodeSphere.Models.User;

namespace CodeSphere.ViewModels.Blog.ViewModels
{
    public class TopPostsViewModel
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? ImageUrl { get; set; }

        public PostStatus PostStatus { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
