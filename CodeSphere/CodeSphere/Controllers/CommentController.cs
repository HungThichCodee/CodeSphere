using CodeSphere.ApplicationAttributes.ActionAttributes;
using CodeSphere.ApplicationAttributes.Blog.Comment;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.Repositories.CommentRepositories;
using CodeSphere.ViewModels.CommentViewModels.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(
            ICommentRepository commentsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsRepository = commentsRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [UserBlocked("Index", "Profile")]
        [CommentCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToCreateComment)]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var parentId = input.ParentId == "0" ? null : input.ParentId;
                if (parentId != null)
                {
                    if (!this.commentsRepository.IsInPostId(parentId, input.PostId))
                    {
                        this.TempData["Error"] = ErrorMessages.DontMakeBullshits;
                        return this.RedirectToAction("Index", "Post", new { postId = input.PostId });
                    }

                    bool isParentApproved = await this.commentsRepository.IsParentCommentApproved(parentId);
                    if (!isParentApproved)
                    {
                        this.TempData["Error"] = ErrorMessages.CannotCommentNotApprovedComment;
                        return this.RedirectToAction("Index", "Post", new { postId = input.PostId });
                    }
                }

                var currentUser = await this.userManager.GetUserAsync(this.User);

                if (await this.commentsRepository.IsPostApproved(input.PostId))
                {
                    this.TempData["Error"] = ErrorMessages.CannotCommentNotApprovedBlogPost;
                    return this.RedirectToAction("Index", "Post", new { postId = input.PostId });
                }

                var tuple = await this.commentsRepository
                    .Create(input.PostId, currentUser, input.SanitizedContent, parentId);
                this.TempData[tuple.Item1] = tuple.Item2;

                return this.RedirectToAction("Index", "Post", new { postId = input.PostId });
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "Blog");
        }

        /// <summary>
        /// This function will delete a comment for a target Blog Post.
        /// </summary>
        /// <param name="commentId">Target comment ID.</param>
        /// <param name="postId">Target post ID related to the comment ID.</param>
        /// <returns>Redirect to Action based on IF-ELSE statements.</returns>
        [HttpPost]
        [Route("/Comment/DeleteById/{commentId}/{postId}")]
        [UserBlocked("Index", "Profile")]
        [CommentCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToDeleteComment)]
        public async Task<IActionResult> DeleteById(string commentId, string postId)
        {
            var tuple = await this.commentsRepository.DeleteCommentById(commentId);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Post", new { postId });
        }

        [HttpGet]
        [Route("/Comment/EditComment/{commentId}/{postId}")]
        [UserBlocked("Index", "Profile")]
        [CommentCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToEditComment)]
        public async Task<IActionResult> EditComment(string commentId, string postId)
        {
            var isCommentIdCorrect = await this.commentsRepository.IsCommentIdCorrect(commentId, postId);

            if (!isCommentIdCorrect)
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.RedirectToAction("Index", "Post", new { postId });
            }

            EditCommentInputModel model = await this.commentsRepository.GetCommentById(commentId);

            return this.View(model);
        }

        [HttpPost]
        [Route("/Comment/EditComment/{commentId}/{postId}")]
        [UserBlocked("Index", "Profile")]
        [CommentCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToEditComment)]
        public async Task<IActionResult> EditComment(string commentId, string postId, EditCommentInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var isCommentIdCorrect = await this.commentsRepository.IsCommentIdCorrect(commentId, postId);

                if (!isCommentIdCorrect)
                {
                    this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                    return this.RedirectToAction("Index", "Post", new { postId });
                }

                Tuple<string, string> tuple = await this.commentsRepository.EditComment(model);
                this.TempData[tuple.Item1] = tuple.Item2;

                return this.RedirectToAction("Index", "Post", new { postId });
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.RedirectToAction("Index", "Blog");
        }
    }
}
