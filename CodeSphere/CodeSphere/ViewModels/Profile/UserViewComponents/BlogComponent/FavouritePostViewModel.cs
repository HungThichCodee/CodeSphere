namespace CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent
{
    public class FavouritePostViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsFavourite { get; set; }

        public BlogComponentCategoryViewModel Category { get; set; }

        public BlogComponentApplicationUserViewModel ApplicationUser { get; set; }
    }
}