using CodeSphere.Areas.Administration.ViewModels.UsersInformation;

namespace CodeSphere.Areas.Administration.Repositories.UsersInformation
{
    public interface IUsersInformationRepository
    {
        Task<AllUsersViewModel> GetAllUsers();

        Task<AllBannedUsersViewModel> GetAllBannedUsers();
    }
}
