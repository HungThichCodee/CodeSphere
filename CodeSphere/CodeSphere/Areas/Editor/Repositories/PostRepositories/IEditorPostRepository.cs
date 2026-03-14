using CodeSphere.Models.User;
using CodeSphere.Repositories;

namespace CodeSphere.Areas.Editor.Repositories.PostRepositories
{
    public interface IEditorPostRepository
    {
        Task<bool> ApprovePost(string id, ApplicationUser currentUser);

        Task<bool> BannPost(string id, ApplicationUser currentUser);

        Task<bool> UnbannPost(string id, ApplicationUser currentUser);
    }
}
