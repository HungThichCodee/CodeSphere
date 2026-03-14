using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage
{
    public class PostCommentViewModel
    {
        public string? Id { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public CommentStatus CommentStatus { get; set; }

        public string? ApplicationUserId { get; set; }

        public PostApplicationUserViewModel? ApplicationUser { get; set; }

        public string? PostId { get; set; }

        public string? ParentCommentId { get; set; }

        public PostCommentViewModel? ParentComment { get; set; }
    }
}