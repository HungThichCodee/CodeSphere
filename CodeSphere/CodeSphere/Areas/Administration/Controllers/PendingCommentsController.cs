using CodeSphere.Areas.Administration.Repositories.PendingComments;
using CodeSphere.Areas.Administration.ViewModels.PendingCommentsViewModels;
using CodeSphere.Constraints;
using CodeSphere.MlModels.CommentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class PendingCommentsController : Controller
    {
        private readonly IPendingCommentsRepository pendingCommentsRepository;
        private readonly PredictionEnginePool<BlogCommentModelInput, BlogCommentModelOutput> predictionEngine;

        public PendingCommentsController(
            IPendingCommentsRepository pendingCommentsRepository,
            PredictionEnginePool<BlogCommentModelInput, BlogCommentModelOutput> predictionEngine)
        {
            this.pendingCommentsRepository = pendingCommentsRepository;
            this.predictionEngine = predictionEngine;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<AdminPendingCommentViewModel> model =
                await this.pendingCommentsRepository.ExtractAllPendingComments(this.predictionEngine);
            return this.View(model);
        }
    }
}
