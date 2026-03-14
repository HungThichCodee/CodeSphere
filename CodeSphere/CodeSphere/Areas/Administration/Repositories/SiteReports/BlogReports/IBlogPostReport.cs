using CodeSphere.Areas.Administration.ViewModels.SiteReportsViewModels.BlogReports;

namespace CodeSphere.Areas.Administration.Repositories.SiteReports.BlogReports
{
    public interface IBlogPostReport
    {
        Task<ICollection<BlogPostReportViewModel>> GetPostsData();

        Task<ICollection<BlogCommentReportViewModel>> GetCommentsData();
    }
}
