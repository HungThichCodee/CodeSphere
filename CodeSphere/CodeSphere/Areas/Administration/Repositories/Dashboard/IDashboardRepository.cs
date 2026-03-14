using CodeSphere.Areas.Administration.ViewModels.DashboardViewModels;
using Microsoft.AspNetCore.Identity;

namespace CodeSphere.Areas.Administration.Repositories.Dashboard
{
    public interface IDashboardRepository
    {
        DashboardViewModel GetDashboardInformation();

        Task<IdentityResult> CreateRole(string role);

        Task<bool> IsAddedUserInRole(string inputRole, string inputUsername);

        Task<bool> RemoveUserFromRole(string username, string role);

        Task<bool> SyncFollowUnfollow();
    }
}
