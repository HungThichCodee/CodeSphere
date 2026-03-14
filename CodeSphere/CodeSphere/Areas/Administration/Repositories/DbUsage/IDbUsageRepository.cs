using CodeSphere.Models.Enums;

namespace CodeSphere.Areas.Administration.Repositories.DbUsage
{
    public interface IDbUsageRepository
    {
        Task<bool> RemoveActivitiesByName(UserActionType actionValue);

        Task<int> RemoveAllActivities();

        ICollection<string> GetAllUsernames();

        Task<bool> DeleteUserImagesByUsername(string username);

        Task<int> DeleteAllUsersImages();
    }
}
