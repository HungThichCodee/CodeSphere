namespace CodeSphere.ViewModels.AllCategories.ViewModels
{
    public class AllCategoriesCategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string Description { get; set; }

        public int BannedPostsCount { get; set; }

        public int PendingPostsCount { get; set; }

        public int ApprovedPostsCount { get; set; }

        public ICollection<AllCategoriesPostViewModel> Posts { get; set; } = new HashSet<AllCategoriesPostViewModel>();
    }
}
