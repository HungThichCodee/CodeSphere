namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class AllUsersUserCardViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingsCount { get; set; }

        public int Activities { get; set; }

        public bool HasFollowed { get; set; }
    }
}