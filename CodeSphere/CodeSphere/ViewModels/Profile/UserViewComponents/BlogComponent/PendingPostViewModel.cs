namespace CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent
{
    public class PendingPostViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public BlogComponentCategoryViewModel Category { get; set; }

        public BlogComponentApplicationUserViewModel ApplicationUser { get; set; }
    }
}