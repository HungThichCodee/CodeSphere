namespace CodeSphere.Areas.Administration.ViewModels.DashboardViewModels
{
    public class DashboardViewModel
    {
        public int? TotalUsersCount { get; set; }

        public int? TotalBlogPosts { get; set; }

        public int? TotalBannedUsers { get; set; }

        public int? TotalUsersInAdminRole { get; set; }

        public int TotalShopProducts { get; set; }

        public int TotalOrdersCount { get; set; }

        public ICollection<string?> Usernames { get; set; } = new HashSet<string?>();
    }
}
