using System.Text.RegularExpressions;
using CodeSphere.Areas.Administration.ViewModels.PendingPostsViewModels;
using CodeSphere.Data;
using CodeSphere.MlModels.PostModels;
using CodeSphere.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Repositories.PendingPosts
{
    public class PendingPostsRepository : IPendingPostsRepository
    {
        private readonly ApplicationDbContext db;

        public PendingPostsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<AdminPendingPostViewModel>> ExtractAllPendingPosts(
            PredictionEnginePool<BlogPostModelInput, BlogPostModelOutput> predictionEngine)
        {
            var pendingPosts = this.db.Posts.Where(x => x.PostStatus == PostStatus.Pending).ToList();
            List<AdminPendingPostViewModel> model = new List<AdminPendingPostViewModel>();

            foreach (var post in pendingPosts)
            {
                var contentWithoutTags = Regex.Replace(post.Content, "<.*?>", string.Empty);
                var prediction = predictionEngine.Predict(new BlogPostModelInput
                {
                    Content = contentWithoutTags,
                });

                model.Add(new AdminPendingPostViewModel
                {
                    Post = post,
                    User = await this.db.Users.FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId),
                    MlPrediction = prediction.Prediction,
                    MlScore = (decimal)prediction.Score[0],
                });
            }

            return model;
        }
    }
}
