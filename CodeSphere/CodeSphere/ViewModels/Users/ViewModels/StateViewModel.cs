namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class StateViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CountryId { get; set; }

        public CountryViewModel Country { get; set; }

        public ICollection<CityViewModel> Cities { get; set; } = new HashSet<CityViewModel>();

        public ICollection<ApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ApplicationUserViewModel>();
    }
}