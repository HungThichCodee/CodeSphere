using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.InputModels;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.InputModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels;

namespace CodeSphere.Repositories.BlogRepositories
{
    public interface IBlogRepository
    {
        Task<ICollection<string>> ExtractAllCategoryNames();

        Task<ICollection<string>> ExtractAllTagNames();

        Task<Tuple<string, string>> CreatePost(CreatePostIndexModel model, ApplicationUser user);

        Task<ICollection<BlogPostCardViewModel>> ExtraxtAllPosts(ApplicationUser user, string search);

        Task<Tuple<string, string>> DeletePost(string id, ApplicationUser user);

        Task<EditPostInputModel> ExtractPost(string id);

        Task<Tuple<string, string>> EditPost(EditPostInputModel model, ApplicationUser user);

        Task<bool> IsPostExist(string id);
    }
}
