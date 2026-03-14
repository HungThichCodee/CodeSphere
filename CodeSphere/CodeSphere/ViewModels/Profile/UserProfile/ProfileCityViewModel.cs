namespace CodeSphere.ViewModels.Profile.UserProfile
{
    public class ProfileCityViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string StateId { get; set; }

        public ProfileStateViewModel State { get; set; }

        public string CountryId { get; set; }

        public ProfileCountryViewModel Country { get; set; }

        public ICollection<ProfileZipCodeViewModel> ZipCodes { get; set; } = new HashSet<ProfileZipCodeViewModel>();

        public ICollection<ProfileApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ProfileApplicationUserViewModel>();
    }
}