namespace CodeSphere.ViewModels.Profile.UserProfile
{
    public class ProfileCountryCodeViewModel
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public ICollection<ProfileCountryViewModel> Coutries { get; set; } = new HashSet<ProfileCountryViewModel>();

        public ICollection<ProfileApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ProfileApplicationUserViewModel>();
    }
}