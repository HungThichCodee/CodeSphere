using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfilePendingPostsRepository
    {
        Task<List<PendingPostViewModel>> ExtractPendingPosts(ApplicationUser user, string currentUserId);
    }
}
