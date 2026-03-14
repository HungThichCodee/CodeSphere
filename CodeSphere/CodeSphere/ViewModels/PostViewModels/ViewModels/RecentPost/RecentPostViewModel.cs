using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.PostViewModels.ViewModels.RecentPost
{
    public class RecentPostViewModel
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? ImageUrl { get; set; }

        public PostStatus PostStatus { get; set; }

        public RecentPostApplicationUserViewModel? ApplicationUser { get; set; }
    }
}