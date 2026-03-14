using CodeSphere.Models.User;

namespace CodeSphere.Areas.Administration.Repositories.UserPenalties
{
    public interface IUsersPenaltiesRepository
    {
        ICollection<string> GetAllBlockedUsers();

        Task<ICollection<string>> GetAllNotBlockedUsers();

        Task<bool> BlockUser(string username, ApplicationUser currentUser, string reasonToBeBlocked);

        Task<bool> UnblockUser(string username, ApplicationUser currentUser);

        Task<int> BlockAllUsers();

        Task<int> UnblockAllUsers();
    }
}
