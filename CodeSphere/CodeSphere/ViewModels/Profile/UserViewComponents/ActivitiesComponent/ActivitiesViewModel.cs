using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent
{
    public class ActivitiesViewModel
    {
        public string Id { get; set; }

        public ActivitiesApplicationUserViewModel ApplicationUser { get; set; }

        public UserActionType Action { get; set; }

        public DateTime ActionDate { get; set; }

        public string PersonUsername { get; set; }

        public string FollowerUsername { get; set; }

        public string ProfileImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public string PostId { get; set; }

        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public UserActionStatus ActionStatus { get; set; }
    }
}