namespace CodeSphere.ViewModels.Profile.UserProfile
{
    public class ProfileStateViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CountryId { get; set; }

        public ProfileCountryViewModel Country { get; set; }

        public ICollection<ProfileCityViewModel> Cities { get; set; } = new HashSet<ProfileCityViewModel>();

        public ICollection<ProfileApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ProfileApplicationUserViewModel>();
    }
}