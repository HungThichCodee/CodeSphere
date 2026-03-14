using CodeSphere.Constraints;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.BannedUsers;
using CodeSphere.ViewModels.Pagination.AllUsers;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.AllUsers
{
    public class BannedUsersViewComponent : ViewComponent
    {
        private readonly IBannedUsersRepository bannedUsersRepository;

        public BannedUsersViewComponent(IBannedUsersRepository bannedUsersRepository)
        {
            this.bannedUsersRepository = bannedUsersRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page, string search)
        {
            List<AllUsersUserCardViewModel> allActivities = await this.bannedUsersRepository.ExtractAllUsers(username, search);

            BannedUsersPaginationViewModel model = new BannedUsersPaginationViewModel
            {
                AllUsers = allActivities.ToPagedList(page, GlobalConstants.UsersCountOnPage),
            };

            return this.View(model);
        }
    }
}
