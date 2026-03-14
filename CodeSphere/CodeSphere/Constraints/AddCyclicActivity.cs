using System;
using System.Linq;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;

namespace CodeSphere.Constraints
{
    public class AddCyclicActivity
    {
        private readonly ApplicationDbContext db;

        public AddCyclicActivity(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddUserAction(ApplicationUser user, UserActionType action, ApplicationUser userPost)
        {
            if (this.db.UserActions
                    .Any(x => x.Action == action &&
                    x.ApplicationUserId == userPost.Id &&
                    x.PersonUsername == userPost.UserName &&
                    x.FollowerUsername == user.UserName))
            {
                var targetAction = this.db.UserActions
                    .FirstOrDefault(x => x.Action == action &&
                    x.ApplicationUserId == userPost.Id &&
                    x.PersonUsername == userPost.UserName &&
                    x.FollowerUsername == user.UserName);
                targetAction.ActionDate = DateTime.UtcNow;
                targetAction.ActionStatus = UserActionStatus.Unread;
            }
            else
            {
                this.db.UserActions.Add(new UserAction
                {
                    Action = action,
                    ActionDate = DateTime.UtcNow,
                    ApplicationUserId = userPost.Id,
                    PersonUsername = userPost.UserName,
                    FollowerUsername = user.UserName,
                    ProfileImageUrl = user.ImageUrl,
                    CoverImageUrl = userPost.CoverImageUrl ?? string.Empty,
                    ActionStatus = UserActionStatus.Unread,
                });
            }
        }

        public void AddLikeUnlikeActivity(ApplicationUser user, Post post, UserActionType action, ApplicationUser postUser)
        {
            if (this.db.UserActions
                .Any(x => x.PostId == post.Id &&
                x.ApplicationUserId == user.Id &&
                x.PersonUsername == user.UserName &&
                x.FollowerUsername == postUser.UserName &&
                x.Action == action))
            {
                var targetAction = this.db.UserActions
                    .FirstOrDefault(x => x.PostId == post.Id &&
                    x.ApplicationUserId == user.Id &&
                    x.PersonUsername == user.UserName &&
                    x.FollowerUsername == postUser.UserName &&
                    x.Action == action);
                targetAction.ActionDate = DateTime.UtcNow;
                targetAction.ActionStatus = UserActionStatus.Unread;
            }
            else
            {
                this.db.UserActions.Add(new UserAction
                {
                    Action = action,
                    ActionDate = DateTime.UtcNow,
                    ApplicationUserId = user.Id,
                    PersonUsername = user.UserName,
                    FollowerUsername = postUser.UserName,
                    ProfileImageUrl = postUser.ImageUrl,
                    CoverImageUrl = post?.ImageUrl ?? postUser.CoverImageUrl ?? string.Empty,
                    PostId = post.Id,
                    PostTitle = post.Title,
                    PostContent = post.ShortContent,
                    ActionStatus = UserActionStatus.Unread,
                });
            }
        }
    }
}
