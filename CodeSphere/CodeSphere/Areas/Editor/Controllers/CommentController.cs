using CodeSphere.Areas.Editor.Repositories.CommentRepositories;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Areas.Editor.Controllers
{
    [Authorize(Roles = GlobalConstants.EditorRole + "," + GlobalConstants.AdministratorRole)]
    [Area(GlobalConstants.EditorArea)]
    public class CommentController : Controller
    {
        private readonly IEditorCommentRepository commentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(IEditorCommentRepository commentRepository, UserManager<ApplicationUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> ApproveComment(string commentId, string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.IsBlocked)
            {
                this.TempData["Error"] = ErrorMessages.YouAreBlock;
                return this.RedirectToAction("Index", "Blog");
            }

            bool isApproved = await this.commentRepository.ApprovedCommentById(commentId, currentUser);

            if (isApproved)
            {
                this.TempData["Success"] = SuccessMessages.SuccessfullyApprovedComment;
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            }

            return this.RedirectToAction("Index", "Post", new { postId = postId });
        }
    }
}
