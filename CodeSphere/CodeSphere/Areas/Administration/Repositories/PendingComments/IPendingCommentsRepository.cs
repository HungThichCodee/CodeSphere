using CodeSphere.Areas.Administration.ViewModels.PendingCommentsViewModels;
using CodeSphere.MlModels.CommentModels;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Repositories.PendingComments
{
    public interface IPendingCommentsRepository
    {
        Task<ICollection<AdminPendingCommentViewModel>> ExtractAllPendingComments(
             PredictionEnginePool<BlogCommentModelInput, BlogCommentModelOutput> predictionEngine);
    }
}
