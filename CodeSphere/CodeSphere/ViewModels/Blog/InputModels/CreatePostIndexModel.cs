namespace CodeSphere.ViewModels.Blog.InputModels
{
    public class CreatePostIndexModel
    {
        public CreatePostInputModel? PostInputModel { get; set; }

        public ICollection<string> Categories { get; set; } = new HashSet<string>();

        public ICollection<string> Tags { get; set; } = new HashSet<string>();
    }
}
