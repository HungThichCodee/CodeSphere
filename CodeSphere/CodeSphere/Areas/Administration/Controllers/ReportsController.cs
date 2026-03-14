using CodeSphere.Areas.Administration.Repositories.SiteReports.BlogReports;
using CodeSphere.Areas.Administration.ViewModels.SiteReportsViewModels.BlogReports;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace CodeSphere.Areas.Administration.Controllers
{
    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class ReportsController : Controller
    {
        private readonly IBlogPostReport blogPostReport;

        public ReportsController(IBlogPostReport blogPostReport)
        {
            this.blogPostReport = blogPostReport;
        }

        public async Task<IActionResult> BlogPostsReport()
        {
            ICollection<BlogPostReportViewModel> posts = await this.blogPostReport.GetPostsData();

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(posts, true);
                package.Save();
            }

            stream.Position = 0;
            string excelName = $"Blog Posts Report - {DateTime.Now:dd-MMMM-yyyy}.xlsx";
            return this.File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                excelName);
        }

        public async Task<IActionResult> BlogCommentsReport()
        {
            ICollection<BlogCommentReportViewModel> comments = await this.blogPostReport.GetCommentsData();

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(comments, true);
                package.Save();
            }

            stream.Position = 0;
            string excelName = $"Blog Comments Report - {DateTime.Now:dd-MMMM-yyyy}.xlsx";
            return this.File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                excelName);
        }
    }
}
