using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.BannedUsers
{
    public interface IBannedUsersRepository
    {
        Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search);
    }
}
