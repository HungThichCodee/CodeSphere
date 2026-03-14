using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public interface IProfileActivitiesRepository
    {
        Task<List<ActivitiesViewModel>> ExtractActivities(string username);

    }
}
