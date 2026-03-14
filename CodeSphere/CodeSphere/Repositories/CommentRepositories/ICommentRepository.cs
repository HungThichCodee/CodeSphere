using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.CommentViewModels.InputModels;

namespace CodeSphere.Repositories.CommentRepositories
{
    public interface ICommentRepository
    {
        Task<Tuple<string, string>> Create(string postId, ApplicationUser user, string content, string parentId);

        bool IsInPostId(string parentId, string postId);

        Task<Tuple<string, string>> DeleteCommentById(string commentId);

        Task<bool> IsPostApproved(string postId);

        Task<bool> IsParentCommentApproved(string parentId);

        Task<bool> IsCommentIdCorrect(string commentId, string postId);

        Task<EditCommentInputModel> GetCommentById(string commentId);

        Task<Tuple<string, string>> EditComment(EditCommentInputModel model);
    }
}
