using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeSphere.ApplicationAttributes.Blog.Post
{
    public class PostCrudOperationsAttribute : BlogRoleAttribute
    {
        private readonly object routValues;
        private readonly string message;

        public PostCrudOperationsAttribute(string actionName, string controllerName, object routValues, string message)
            : base(actionName, controllerName)
        {
            this.routValues = routValues;
            this.message = message;
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

            if (context.ActionArguments.ContainsKey("postId"))
            {
                var postId = context.ActionArguments["postId"]?.ToString();
                var post = db.Posts.FirstOrDefault(x => x.Id == postId);
                var userPostsIds = db.Posts.Where(x => x.ApplicationUserId == user.Id).Select(x => x.Id).ToList();

                var controller = context.Controller as Controller;

                var isOwnPost = userPostsIds.Contains(postId);
                var hasFullControl = userManager.IsInRoleAsync(user, Roles.Administrator.ToString()).Result ||
                    userManager.IsInRoleAsync(user, Roles.Editor.ToString()).Result;

                if (post == null)
                {
                    controller.TempData["Error"] = ErrorMessages.NotExistingPost;
                    context.Result = new RedirectToActionResult(
                        this.ActionName,
                        this.ControllerName,
                        this.routValues);
                }
                else if (!isOwnPost && !hasFullControl)
                {
                    controller.TempData["Error"] = this.message;
                    context.Result = new RedirectToActionResult(
                        this.ActionName,
                        this.ControllerName,
                        this.routValues);
                }
            }
        }
    }
}
