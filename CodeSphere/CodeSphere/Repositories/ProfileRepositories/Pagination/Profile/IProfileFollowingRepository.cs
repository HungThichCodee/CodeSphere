using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfileFollowingRepository
    {
        Task<List<FollowingViewModel>> ExtractFollowing(string username);
    }
}
