namespace CodeSphere.ViewModels.Profile.UserProfile
{
    public class ProfileZipCodeViewModel
    {
        public string Id { get; set; }

        public int Code { get; set; }

        public string CityId { get; set; }

        public ProfileCityViewModel City { get; set; }

        public ICollection<ProfileApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ProfileApplicationUserViewModel>();
    }
}