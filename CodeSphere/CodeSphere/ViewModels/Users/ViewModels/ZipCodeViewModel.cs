namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class ZipCodeViewModel
    {
        public string Id { get; set; }

        public int Code { get; set; }

        public string CityId { get; set; }

        public CityViewModel City { get; set; }

        public ICollection<ApplicationUserViewModel> ApplicationUsers { get; set; } = new HashSet<ApplicationUserViewModel>();
    }
}