using System.Text.RegularExpressions;
using CodeSphere.Areas.Administration.ViewModels.PendingCommentsViewModels;
using CodeSphere.Data;
using CodeSphere.MlModels.CommentModels;
using CodeSphere.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;

namespace CodeSphere.Areas.Administration.Repositories.PendingComments
{
    public class PendingCommentsRepository : IPendingCommentsRepository
    {
        private readonly ApplicationDbContext db;

        public PendingCommentsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<AdminPendingCommentViewModel>> ExtractAllPendingComments(
            PredictionEnginePool<BlogCommentModelInput, BlogCommentModelOutput> predictionEngine)
        {
            var pendingComments = this.db.Comments.Where(x => x.CommentStatus == CommentStatus.Pending).ToList();
            List<AdminPendingCommentViewModel> model = new List<AdminPendingCommentViewModel>();

            foreach (var comment in pendingComments)
            {
                var contentWithoutTags = Regex.Replace(comment.Content, "<.*?>", string.Empty);
                var prediction = predictionEngine.Predict(new BlogCommentModelInput
                {
                    Content = contentWithoutTags,
                });

                var targetComment = new AdminPendingCommentViewModel
                {
                    Comment = comment,
                    User = await this.db.Users.FirstOrDefaultAsync(x => x.Id == comment.ApplicationUserId),
                    MlPrediction = prediction.Prediction,
                    MlScore = (decimal)prediction.Score[0],
                };

                targetComment.Comment.Post = await this.db.Posts.FirstOrDefaultAsync(x => x.Id == comment.PostId);
                model.Add(targetComment);
            }

            return model;
        }
    }
}
