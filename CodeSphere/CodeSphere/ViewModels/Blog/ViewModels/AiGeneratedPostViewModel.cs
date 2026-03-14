using CodeSphere.Models.Blog;

namespace CodeSphere.ViewModels.Blog.ViewModels
{
    public class AiGeneratedPostViewModel
    {
        public string Topic { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public ICollection<Post> SimilarPosts { get; set; } = new HashSet<Post>();

        public string? ExtractedTitle { get; set; }
    }
}