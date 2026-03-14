using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeSphere.ApplicationAttributes.Blog.Post
{
    public class PostActionsAttribute : BlogRoleAttribute
    {
        public PostActionsAttribute(string actionName, string controllerName, object routValues, string message)
            : base(actionName, controllerName)
        {
            this.RoutValues = routValues;
            this.Message = message;
        }

        public object RoutValues { get; }

        public string Message { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var db = context
                .HttpContext
                .RequestServices
                .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            if (context.ActionArguments.ContainsKey("postId"))
            {
                var postId = context.ActionArguments["postId"].ToString();
                var post = db.Posts.FirstOrDefault(x => x.Id == postId);

                var controller = context.Controller as Controller;

                if (post == null)
                {
                    controller.TempData["Error"] = ErrorMessages.NotExistingPost;
                    context.Result = new RedirectToActionResult(
                        this.ActionName,
                        this.ControllerName,
                        this.RoutValues);
                }
                else if (post.PostStatus != PostStatus.Approved)
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
