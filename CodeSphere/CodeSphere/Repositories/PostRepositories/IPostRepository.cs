using CodeSphere.Models.User;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;

namespace CodeSphere.Repositories.PostRepositories
{
    public interface IPostRepository
    {
        Task<Tuple<string, string>> LikePost(string id, ApplicationUser user);

        Task<PostViewModel> ExtractCurrentPost(string postId, ApplicationUser user);

        Task<Tuple<string, string>> UnlikePost(string id, ApplicationUser user);

        Task<Tuple<string, string>> AddToFavorite(ApplicationUser user, string id);

        Task<Tuple<string, string>> RemoveFromFavorite(ApplicationUser user, string id);

        Task<bool> IsPostExist(string id);
    }
}
