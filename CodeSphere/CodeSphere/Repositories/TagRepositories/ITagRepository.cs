using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.TagViewModels.TagPage;

namespace CodeSphere.Repositories.TagRepositories
{
    public interface ITagRepository
    {
        Task<TagPageTagViewModel> ExtractTagById(string id);

        Task<ICollection<BlogPostCardViewModel>> ExtractPostsByTagId(string id, ApplicationUser user);
    }
}
