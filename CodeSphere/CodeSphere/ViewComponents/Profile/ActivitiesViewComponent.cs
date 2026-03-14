using CodeSphere.Constraints;
using CodeSphere.Repositories.ProfileRepositories.Pagination.Profile;
using CodeSphere.ViewModels.Pagination.Profile;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.ViewComponents.Profile
{
    public class ActivitiesViewComponent : ViewComponent
    {
        private readonly IProfileActivitiesRepository activitiesRepository;

        public ActivitiesViewComponent(IProfileActivitiesRepository activitiesRepository)
        {
            this.activitiesRepository = activitiesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, int page)
        {
            List<ActivitiesViewModel> allActivities = await activitiesRepository.ExtractActivities(username);

            ActivitiesPaginationViewModel model = new ActivitiesPaginationViewModel
            {
                Username = username,
                Activities = allActivities.ToPagedList(page, GlobalConstants.UsersActivitiesCountOnPage),
            };

            return View(model);
        }
    }
}
