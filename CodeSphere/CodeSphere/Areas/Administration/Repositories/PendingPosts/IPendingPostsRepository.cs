using CodeSphere.Areas.Administration.ViewModels.PendingPostsViewModels;
using CodeSphere.MlModels.PostModels;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Repositories.PendingPosts
{
    public interface IPendingPostsRepository
    {
        Task<ICollection<AdminPendingPostViewModel>> ExtractAllPendingPosts(
            PredictionEnginePool<BlogPostModelInput, BlogPostModelOutput> predictionEngine);
    }
}