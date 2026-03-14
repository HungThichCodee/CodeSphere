using CodeSphere.Models.Blog;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.Administration.ViewModels.PendingPostsViewModels
{
    public class AdminPendingPostViewModel
    {
        public Post Post { get; set; }

        public ApplicationUser User { get; set; }

        public string MlPrediction { get; set; }

        public decimal MlScore { get; set; }
    }
}
