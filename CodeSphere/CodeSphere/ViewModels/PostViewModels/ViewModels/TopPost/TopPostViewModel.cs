using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.PostViewModels.ViewModels.TopPost
{
    public class TopPostViewModel
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? ImageUrl { get; set; }

        public PostStatus PostStatus { get; set; }

        public TopPostApplicationUserViewMdoel? ApplicationUser { get; set; }
    }
}