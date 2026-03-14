using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfileFollowersRepository
    {
        Task<List<FollowersViewModel>> ExtractFollowers(string username);
    }
}
