namespace CodeSphere.Areas.Administration.ViewModels.AllHolidayThemes.ViewModels
{
    public class AllHolidayThemesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<string> IconsUrls { get; set; } = new HashSet<string>();
    }
}
