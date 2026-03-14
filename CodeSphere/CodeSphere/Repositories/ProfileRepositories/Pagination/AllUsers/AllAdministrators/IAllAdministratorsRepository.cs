using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllAdministrators
{
    public interface IAllAdministratorsRepository
    {
        Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search);
    }
}
