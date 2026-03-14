using CodeSphere.Models.Enums;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public ProfileTab ActiveTab { get; set; }

        public ProfileApplicationUserViewModel ApplicationUser { get; set; }

        public bool HasAdmin { get; set; }

        public int Page { get; set; }

        public double RatingScore { get; set; }

        public int LatestScore { get; set; }
    }
}