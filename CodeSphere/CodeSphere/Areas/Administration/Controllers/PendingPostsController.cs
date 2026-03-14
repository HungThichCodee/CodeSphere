using CodeSphere.Areas.Administration.Repositories.PendingPosts;
using CodeSphere.Areas.Administration.ViewModels.PendingPostsViewModels;
using CodeSphere.Constraints;
using CodeSphere.MlModels.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class PendingPostsController : Controller
    {
        private readonly IPendingPostsRepository pendingPostsRepository;
        private readonly PredictionEnginePool<BlogPostModelInput, BlogPostModelOutput> predictionEngine;

        public PendingPostsController(
            IPendingPostsRepository pendingPostsRepository,
            PredictionEnginePool<BlogPostModelInput, BlogPostModelOutput> predictionEngine)
        {
            this.pendingPostsRepository = pendingPostsRepository;
            this.predictionEngine = predictionEngine;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<AdminPendingPostViewModel> model =
                await this.pendingPostsRepository.ExtractAllPendingPosts(this.predictionEngine);
            return this.View(model);
        }
    }
}
