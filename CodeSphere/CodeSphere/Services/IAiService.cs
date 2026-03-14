using CodeSphere.ViewModels.Blog.ViewModels;

namespace CodeSphere.Services
{
    public interface IAiService
    {
        Task<string> GeneratePostContentAsync(string topic, string? additionalContext = null);

        Task<AiGeneratedPostViewModel> GeneratePostAsync(string topic, string? additionalContext = null);
    }
}