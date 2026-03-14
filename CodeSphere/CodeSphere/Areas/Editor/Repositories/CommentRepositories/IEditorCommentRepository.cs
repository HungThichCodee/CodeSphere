using CodeSphere.Models.User;

namespace CodeSphere.Areas.Editor.Repositories.CommentRepositories
{
    public interface IEditorCommentRepository
    {
        Task<bool> ApprovedCommentById(string commentId, ApplicationUser currentUser);
    }
}
