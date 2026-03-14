using CodeSphere.Models.User;
using CodeSphere.ViewModels.Home;
using Microsoft.AspNetCore.Identity;

namespace CodeSphere.Repositories
{
    public interface IHomeRepository
    {
        int GetRegisteredUsersCount();

        Task<IdentityResult> CreateRole(string role);

        Task<ICollection<HomeAdministratorUserViewModel>> GetAllAdministrators();

        ICollection<HomeLatestPostViewModel> GetLatestPosts();

        int GetPostsCount();

        Task<ICollection<string>> GetHolidayThemeIcons();
    }
}
