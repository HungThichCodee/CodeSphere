using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Data;
using CodeSphere.Hubs;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Editor.Repositories.PostRepositories
{
    public class EditorPostRepository : IEditorPostRepository
    {
        private readonly ApplicationDbContext db;
        private readonly INotificationRepository notificationRepository;
        private readonly IHubContext<NotificationHub> notificationHubContext;

        private readonly List<UserActionType> actionsForBann = new List<UserActionType>()
        {
            UserActionType.LikedPost,
            UserActionType.LikePost,
            UserActionType.LikeOwnPost,
            UserActionType.EditedPost,
            UserActionType.EditPost,
            UserActionType.EditOwnPost,
            UserActionType.CreatePost,
            UserActionType.UnlikedPost,
            UserActionType.UnlikePost,
            UserActionType.UnlikeOwnPost,
        };

        public EditorPostRepository(
            ApplicationDbContext db,
            INotificationRepository notificationRepository,
            IHubContext<NotificationHub> notificationHubContext)
        {
            this.db = db;
            this.notificationRepository = notificationRepository;
            this.notificationHubContext = notificationHubContext;
        }

        public async Task<bool> ApprovePost(string id, ApplicationUser currentUser)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                var targetApprovedEntity = this.db.PendingPosts
                    .FirstOrDefault(x => x.PostId == post.Id && x.IsPending == true);
                if (targetApprovedEntity != null)
                {
                    post.PostStatus = PostStatus.Approved;
                    targetApprovedEntity.IsPending = false;
                    this.db.PendingPosts.Update(targetApprovedEntity);
                    this.db.Posts.Update(post);
                    await this.db.SaveChangesAsync();

                    var specialRoleIds = this.db.Roles
                        .Where(x => x.Name == Roles.Administrator.ToString() ||
                        x.Name == Roles.Editor.ToString())
                        .Select(x => x.Id)
                        .ToList();
                    var specialIds = this.db.UserRoles
                        .Where(x => specialRoleIds.Contains(x.RoleId))
                        .Select(x => x.UserId)
                        .ToList();
                    var followerIds = this.db.FollowUnfollows
                    .Where(x => x.ApplicationUserId == post.ApplicationUserId && !specialIds.Contains(x.FollowerId))
                    .Select(x => x.FollowerId)
                    .ToList();

                    foreach (var followerId in followerIds)
                    {
                        var toUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id == followerId);
                        var user = await this.db.Users.FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId);

                        string notificationId =
                            await this.notificationRepository
                            .AddBlogPostNotification(toUser, user, post.ShortContent, post.Id);

                        var count = await this.notificationRepository.GetUserNotificationsCount(toUser.UserName);
                        await this.notificationHubContext
                            .Clients
                            .User(toUser.Id)
                            .SendAsync("ReceiveNotification", count, true);

                        var notification = await this.notificationRepository.GetNotificationById(notificationId);
                        await this.notificationHubContext.Clients.User(toUser.Id)
                            .SendAsync("VisualizeNotification", notification);
                    }

                    var targetUser = await this.db.Users
                        .FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId);
                    string notificationForApprovingId =
                           await this.notificationRepository
                           .AddApprovedPostNotification(targetUser, currentUser, post.ShortContent, post.Id);

                    var targetUserNotificationsCount = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                    await this.notificationHubContext
                        .Clients
                        .User(targetUser.Id)
                        .SendAsync("ReceiveNotification", targetUserNotificationsCount, true);

                    var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationForApprovingId);
                    await this.notificationHubContext.Clients.User(targetUser.Id)
                        .SendAsync("VisualizeNotification", notificationForApproving);

                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<bool> BannPost(string id, ApplicationUser currentUser)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                var targetApprovedEntity = this.db.BlockedPosts
                    .FirstOrDefault(x => x.PostId == post.Id && x.IsBlocked == false);
                if (targetApprovedEntity != null)
                {
                    post.PostStatus = PostStatus.Banned;
                    targetApprovedEntity.IsBlocked = true;
                    var favorites = this.db.FavouritePosts.Where(x => x.PostId == id);

                    foreach (var favor in favorites)
                    {
                        favor.IsFavourite = false;
                    }

                    var likes = this.db.PostsLikes.Where(x => x.PostId == id && x.IsLiked == true);

                    foreach (var like in likes)
                    {
                        like.IsLiked = false;
                        post.Likes--;
                    }

                    var actions = this.db.UserActions
                        .Where(x => x.PostId == id && this.actionsForBann.Contains(x.Action));

                    this.db.FavouritePosts.UpdateRange(favorites);
                    this.db.PostsLikes.UpdateRange(likes);
                    this.db.UserActions.RemoveRange(actions);
                    this.db.BlockedPosts.Update(targetApprovedEntity);
                    this.db.Posts.Update(post);
                    await this.db.SaveChangesAsync();

                    var targetUser = await this.db.Users
                           .FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId);
                    string notificationId =
                           await this.notificationRepository
                           .AddBannPostNotification(targetUser, currentUser, post.ShortContent, post.Id);

                    var count = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                    await this.notificationHubContext
                        .Clients
                        .User(targetUser.Id)
                        .SendAsync("ReceiveNotification", count, true);

                    var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationId);
                    await this.notificationHubContext.Clients.User(targetUser.Id)
                        .SendAsync("VisualizeNotification", notificationForApproving);

                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<bool> UnbannPost(string id, ApplicationUser currentUser)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                var targetApprovedEntity = this.db.BlockedPosts
                    .FirstOrDefault(x => x.PostId == post.Id && x.IsBlocked == true);
                if (targetApprovedEntity != null)
                {
                    post.PostStatus = PostStatus.Approved;
                    targetApprovedEntity.IsBlocked = false;
                    this.db.BlockedPosts.Update(targetApprovedEntity);
                    this.db.Posts.Update(post);
                    await this.db.SaveChangesAsync();

                    var targetUser = await this.db.Users
                           .FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId);
                    string notificationId =
                           await this.notificationRepository
                           .AddUnbannPostNotification(targetUser, currentUser, post.ShortContent, post.Id);

                    var count = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                    await this.notificationHubContext
                        .Clients
                        .User(targetUser.Id)
                        .SendAsync("ReceiveNotification", count, true);

                    var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationId);
                    await this.notificationHubContext.Clients.User(targetUser.Id)
                        .SendAsync("VisualizeNotification", notificationForApproving);

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
