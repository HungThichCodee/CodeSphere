using CodeSphere.Constraints;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllUsersTab;
using CodeSphere.ViewModels.Pagination.AllUsers;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.AllUsers
{
    public class AllUsersViewComponent : ViewComponent
    {
        private readonly IAllUsersRepository allUsersRepository;

        public AllUsersViewComponent(IAllUsersRepository allUsersRepository)
        {
            this.allUsersRepository = allUsersRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page, string search)
        {
            List<AllUsersUserCardViewModel> allUsers = await this.allUsersRepository.ExtractAllUsers(username, search);

            AllUsersPaginationViewModel model = new AllUsersPaginationViewModel
            {
                AllUsers = allUsers.ToPagedList(page, GlobalConstants.UsersCountOnPage),
            };

            return this.View(model);
        }
    }
}
