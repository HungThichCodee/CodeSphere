using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.CommentViewModels.ViewModels
{
    public class RecentCommentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public CommentStatus CommentStatus { get; set; }

        public RecentCommentApplicationUserViewModel ApplicationUser { get; set; }

        public string PostId { get; set; }
    }
}
