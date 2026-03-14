using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllUsersTab
{
    public interface IAllUsersRepository
    {
        Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search);
    }
}
