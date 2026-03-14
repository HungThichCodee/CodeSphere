namespace CodeSphere.Areas.Administration.ViewModels.DbUsageViewModels.DeleteUsersImages
{
    public class DeleteUsersImagesViewModel
    {
        public DeleteImagesByUsernameInputModel? DeleteUserImages { get; set; }

        public ICollection<string> Usernames { get; set; } = new HashSet<string>();
    }
}
