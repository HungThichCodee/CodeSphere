namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class ManageAccountViewModel
    {
        public ICollection<string> CountryCodes { get; set; } = new HashSet<string>();

        public ICollection<string> Cities { get; set; } = new HashSet<string>();

        public ICollection<string> States { get; set; } = new HashSet<string>();

        public ICollection<string> Countries { get; set; } = new HashSet<string>();
    }
}