using CodeSphere.Models.Blog;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.Administration.ViewModels.PendingCommentsViewModels
{
    public class AdminPendingCommentViewModel
    {
        public Comment Comment { get; set; }

        public ApplicationUser User { get; set; }

        public string MlPrediction { get; set; }

        public decimal MlScore { get; set; }
    }
}
