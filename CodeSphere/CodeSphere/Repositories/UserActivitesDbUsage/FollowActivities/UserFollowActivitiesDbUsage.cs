using CodeSphere.Data;
using CodeSphere.Models.Enums;

namespace CodeSphere.Repositories.UserActivitesDbUsage.FollowActivities
{
    public class UserFollowActivitiesDbUsage : IUserFollowActivitiesDbUsage
    {
        private readonly ApplicationDbContext db;

        public UserFollowActivitiesDbUsage(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void DeleteFollowActivites()
        {
            var target = this.db.UserActions
                 .Where(x => (x.Action == UserActionType.Follow ||
                x.Action == UserActionType.Followed ||
                x.Action == UserActionType.Unfollow ||
                x.Action == UserActionType.Unfollowed) &&
                x.ActionStatus == UserActionStatus.Read)
                .ToList();

            this.db.UserActions.RemoveRange(target);
            this.db.SaveChanges();
        }
    }
}
