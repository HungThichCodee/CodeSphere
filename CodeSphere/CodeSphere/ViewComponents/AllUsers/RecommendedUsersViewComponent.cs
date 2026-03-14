using CodeSphere.Constraints;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.RecommendedUsers;
using CodeSphere.ViewModels.Pagination.AllUsers;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.AllUsers
{
    public class RecommendedUsersViewComponent : ViewComponent
    {
        private readonly IRecommendedUsersRepository recommendedUsersRepository;

        public RecommendedUsersViewComponent(IRecommendedUsersRepository recommendedUsersRepository)
        {
            this.recommendedUsersRepository = recommendedUsersRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page, string search)
        {
            List<AllUsersUserCardViewModel> allActivities = await this.recommendedUsersRepository.ExtractAllUsers(username, search);

            RecommendedUsersPaginationViewModel model = new RecommendedUsersPaginationViewModel
            {
                AllUsers = allActivities.ToPagedList(page, GlobalConstants.UsersCountOnPage),
            };

            return this.View(model);
        }
    }
}
