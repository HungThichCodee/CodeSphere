using CodeSphere.ApplicationAttributes.Blog.Post;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeSphere.ApplicationAttributes.Blog.Comment
{
    public class CommentCrudOperationsAttribute : PostActionsAttribute
    {
        public CommentCrudOperationsAttribute(string actionName, string controllerName, object routValues, string message)
            : base(actionName, controllerName, routValues, message)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var db = context
                .HttpContext
                .RequestServices
                .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            var userManager = context
                .HttpContext
                .RequestServices
                .GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;

            var username = context.HttpContext.User.Identity.Name;
            var user = userManager.FindByNameAsync(username).Result;

            if (context.ActionArguments.ContainsKey("commentId"))
            {
                var commentId = context.ActionArguments["commentId"].ToString();

                var comment = db.Comments.FirstOrDefault(x => x.Id == commentId);
                var userCommentsIds = db.Comments.Where(x => x.ApplicationUserId == user.Id).Select(x => x.Id).ToList();

                var controller = context.Controller as Controller;

                var isOwnComment = userCommentsIds.Contains(commentId);
                var hasFullControl = userManager.IsInRoleAsync(user, Roles.Administrator.ToString()).Result ||
                    userManager.IsInRoleAsync(user, Roles.Editor.ToString()).Result;

                if (comment == null)
                {
                    controller.TempData["Error"] = ErrorMessages.NotExistingComment;
                    context.Result = new RedirectToActionResult(
                        this.ActionName,
                        this.ControllerName,
                        this.RoutValues);
                }
                else if (!isOwnComment && !hasFullControl)
                {
                    controller.TempData["Error"] = this.Message;
                    context.Result = new RedirectToActionResult(
                        this.ActionName,
                        this.ControllerName,
                        this.RoutValues);
                }
            }
        }
    }
}
