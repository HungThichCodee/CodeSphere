namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class CountryCodeViewModel
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public ICollection<CountryViewModel> Coutries { get; set; } = new HashSet<CountryViewModel>();

        public ICollection<ApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ApplicationUserViewModel>();
    }
}