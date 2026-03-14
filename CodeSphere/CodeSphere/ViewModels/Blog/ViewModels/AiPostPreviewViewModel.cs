using CodeSphere.ViewModels.Blog.InputModels;

namespace CodeSphere.ViewModels.Blog.ViewModels
{
    public class AiPostPreviewViewModel
    {
        public GeneratePostInputModel GenerateInput { get; set; } = new GeneratePostInputModel();

        public AiGeneratedPostViewModel GeneratedPost { get; set; } = new AiGeneratedPostViewModel();

        public ICollection<string> Categories { get; set; } = new HashSet<string>();

        public ICollection<string> Tags { get; set; } = new HashSet<string>();
    }
}