using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.RecommendedUsers
{
    public interface IRecommendedUsersRepository
    {
        Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search);
    }
}
