using CodeSphere.Models.Blog;

namespace CodeSphere.Repositories.AiRepositories
{
    public interface IAiRepository
    {
        Task<ICollection<Post>> FindSimilarPostsAsync(string topic, string? content, string? categoryName, ICollection<string>? tags, int count = 5, string? excludePostId = null);
    }
}