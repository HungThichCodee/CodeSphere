using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfileBannedPostsRepository
    {
        Task<List<BannedPostViewModel>> ExtractBannedPosts(ApplicationUser user, string currentUserId);
    }
}
