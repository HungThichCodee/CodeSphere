using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Hubs;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.PostRepositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;
        private readonly AddCyclicActivity cyclicActivity;

        public PostRepository(
            ApplicationDbContext db,
            IHubContext<NotificationHub> notificationHubContext,
            INotificationRepository notificationRepository,
            IMapper mapper)
        {
            this.db = db;
            this.notificationHubContext = notificationHubContext;
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
            this.cyclicActivity = new AddCyclicActivity(this.db);
        }

        public async Task<Tuple<string, string>> AddToFavorite(ApplicationUser user, string id)
        {
            if (user != null && id != null)
            {
                if (this.db.FavouritePosts.Any(x => x.PostId == id && x.ApplicationUserId == user.Id))
                {
                    this.db.FavouritePosts.FirstOrDefault(x => x.PostId == id && x.ApplicationUserId == user.Id).IsFavourite = true;
                }
                else
                {
                    this.db.FavouritePosts.Add(new FavouritePost
                    {
                        ApplicationUserId = user.Id,
                        PostId = id,
                        IsFavourite = true,
                    });
                }

                await this.db.SaveChangesAsync();

                var post = await this.db.Posts.FirstOrDefaultAsync(x => x.Id == id);

                if (post.ApplicationUserId != user.Id)
                {
                    var targetUser = await this.db.Users
                            .FirstOrDefaultAsync(x => x.Id == post.ApplicationUserId);
                    string notificationForApprovingId =
                           await this.notificationRepository
                           .AddPostToFavoriteNotification(targetUser, user, post.ShortContent, post.Id);

                    var targetUserNotificationsCount = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                    await this.notificationHubContext
                        .Clients
                        .User(targetUser.Id)
                        .SendAsync("ReceiveNotification", targetUserNotificationsCount, true);

                    var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationForApprovingId);
                    await this.notificationHubContext.Clients.User(targetUser.Id)
                        .SendAsync("VisualizeNotification", notificationForApproving);
                }

                return Tuple.Create("Success", SuccessMessages.SuccessfullyAddedToFavorite);
            }

            return Tuple.Create("Error", ErrorMessages.InvalidInputModel);
        }

        public async Task<PostViewModel> ExtractCurrentPost(string postId, ApplicationUser user)
        {
            var post = await this.db.Posts
                .Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .Include(x => x.PostLikes.Where(x => x.IsLiked))
                    .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PostImages)
                .Include(x => x.Comments
                    .Where(x => (x.CommentStatus == CommentStatus.Pending && x.ApplicationUserId == user.Id) ||
                                 x.CommentStatus == CommentStatus.Approved)
                    .OrderBy(x => x.CreatedOn).AsQueryable())
                    .ThenInclude(c => c.ApplicationUser)
                .Include(x => x.PostsTags)
                    .ThenInclude(x => x.Tag)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == postId);

            var model = this.mapper.Map<PostViewModel>(post);
            return model;
        }

        public async Task<bool> IsPostExist(string id)
        {
            return await this.db.Posts.AnyAsync(x => x.Id == id);
        }

        public async Task<Tuple<string, string>> LikePost(string id, ApplicationUser user)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);
            post.ApplicationUser = this.db.Users.Find(post.ApplicationUserId);

            if (post != null)
            {
                post.Likes++;
                this.db.Posts.Update(post);
                var targetLike = this.db.PostsLikes.FirstOrDefault(x => x.PostId == id && x.UserId == user.Id);

                if (targetLike != null && targetLike.IsLiked == false)
                {
                    targetLike.IsLiked = true;
                }
                else if (targetLike != null && targetLike.IsLiked == true)
                {
                    targetLike.IsLiked = false;
                }
                else
                {
                    this.db.PostsLikes.Add(new PostLike
                    {
                        UserId = user.Id,
                        PostId = id,
                        IsLiked = true,
                    });
                }

                if (post.ApplicationUserId == user.Id)
                {
                    this.cyclicActivity.AddLikeUnlikeActivity(user, post, UserActionType.LikeOwnPost, user);
                }
                else
                {
                    this.cyclicActivity.AddLikeUnlikeActivity(post.ApplicationUser, post, UserActionType.LikedPost, user);
                    this.cyclicActivity.AddLikeUnlikeActivity(user, post, UserActionType.LikePost, post.ApplicationUser);
                }

                await this.db.SaveChangesAsync();
                return Tuple.Create("Success", SuccessMessages.SuccessfullyLikePost);
            }

            return Tuple.Create("Error", ErrorMessages.InvalidInputModel);
        }

        public async Task<Tuple<string, string>> RemoveFromFavorite(ApplicationUser user, string id)
        {
            if (user != null && id != null)
            {
                if (this.db.FavouritePosts.Any(x => x.PostId == id && x.ApplicationUserId == user.Id))
                {
                    this.db.FavouritePosts.FirstOrDefault(x => x.PostId == id && x.ApplicationUserId == user.Id).IsFavourite = false;
                }
                else
                {
                    return Tuple.Create("Error", ErrorMessages.InvalidInputModel);
                }

                await this.db.SaveChangesAsync();
                return Tuple.Create("Success", SuccessMessages.SuccessfullyRemoveFromFavorite);
            }

            return Tuple.Create("Error", ErrorMessages.InvalidInputModel);
        }

        public async Task<Tuple<string, string>> UnlikePost(string id, ApplicationUser user)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);
            post.ApplicationUser = this.db.Users.Find(post.ApplicationUserId);

            var targetPostsLikes = this.db.PostsLikes.FirstOrDefault(x => x.PostId == id && x.UserId == user.Id);
            if (targetPostsLikes != null && targetPostsLikes.IsLiked == true)
            {
                targetPostsLikes.IsLiked = false;
                post.Likes--;

                if (post.ApplicationUserId == user.Id)
                {
                    this.cyclicActivity.AddLikeUnlikeActivity(user, post, UserActionType.UnlikeOwnPost, user);
                }
                else
                {
                    this.cyclicActivity.AddLikeUnlikeActivity(post.ApplicationUser, post, UserActionType.UnlikedPost, user);
                    this.cyclicActivity.AddLikeUnlikeActivity(user, post, UserActionType.UnlikePost, post.ApplicationUser);
                }

                await this.db.SaveChangesAsync();
                return Tuple.Create("Success", SuccessMessages.SuccessfullyUnlikePost);
            }

            return Tuple.Create("Error", ErrorMessages.InvalidInputModel);
        }
    }
}
