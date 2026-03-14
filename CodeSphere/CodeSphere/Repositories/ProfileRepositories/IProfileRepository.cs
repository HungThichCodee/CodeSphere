using System.Threading.Tasks;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserProfile;
using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Repositories.ProfileRepositories
{
    public interface IProfileRepository
    {
        Task<ProfileApplicationUserViewModel> ExtractUserInfo(string username, ApplicationUser user);

        Task<ApplicationUser> FollowUser(string username, ApplicationUser user);

        Task<ApplicationUser> UnfollowUser(string username, ApplicationUser user);

        Task DeleteActivity(ApplicationUser user);

        Task<string> DeleteActivityById(ApplicationUser user, string activityId);

        Task<bool> HasAdmin(ApplicationRole role);

        Task<bool> HasAdministrator();

        void MakeYourselfAdmin(string username);

        Task<double> RateUser(ApplicationUser currentUser, string username, int rate);

        double ExtractUserRatingScore(string username);

        Task<int> GetLatestScore(ApplicationUser currentUser, string username);

        Task<bool> IsUserExist(string username);

        Task ChangeActionStatus(string username, string id, string status);
    }
}
