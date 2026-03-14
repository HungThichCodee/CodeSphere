using System;
using System.Reflection;
using CodeSphere.Data;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;

namespace CodeSphere.Constraints
{
    public class AddNonCyclicActivity
    {
        private readonly ApplicationDbContext db;

        public AddNonCyclicActivity(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddUserAction(ApplicationUser user, Post post, UserActionType action, ApplicationUser postUser)
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
            });
        }
    }
}
