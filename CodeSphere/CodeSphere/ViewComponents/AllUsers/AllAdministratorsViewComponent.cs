using CodeSphere.Constraints;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllAdministrators;
using CodeSphere.ViewModels.Pagination.AllUsers;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.AllUsers
{
    public class AllAdministratorsViewComponent : ViewComponent
    {
        private readonly IAllAdministratorsRepository allAdministratorsRepository;

        public AllAdministratorsViewComponent(IAllAdministratorsRepository allAdministratorsRepository)
        {
            this.allAdministratorsRepository = allAdministratorsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page, string search)
        {
            List<AllUsersUserCardViewModel> allUsers = await this.allAdministratorsRepository.ExtractAllUsers(username, search);

            AllAdministratorsPaginationViewModel model = new AllAdministratorsPaginationViewModel
            {
                AllUsers = allUsers.ToPagedList(page, GlobalConstants.UsersCountOnPage),
            };

            return this.View(model);
        }
    }
}
