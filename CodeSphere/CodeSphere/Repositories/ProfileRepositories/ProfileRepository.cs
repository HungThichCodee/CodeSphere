using AutoMapper;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Hubs;
using CodeSphere.Models;
using CodeSphere.Models.Enums;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserProfile;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories
{
    public class ProfileRepository : AddCyclicActivity, IProfileRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly INotificationRepository notificationRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMapper mapper;

        public ProfileRepository(
            ApplicationDbContext db,
            IHubContext<NotificationHub> notificationHubContext,
            INotificationRepository notificationRepository,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper)
            : base(db)
        {
            this.db = db;
            this.notificationHubContext = notificationHubContext;
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        public async Task DeleteActivity(ApplicationUser user)
        {
            var trash = this.db.UserActions.Where(x => x.ApplicationUserId == user.Id).ToList();
            this.db.UserActions.RemoveRange(trash);
            await this.db.SaveChangesAsync();
        }

        public async Task<string> DeleteActivityById(ApplicationUser user, string activityId)
        {
            var trash = this.db.UserActions.FirstOrDefault(x => x.ApplicationUserId == user.Id && x.Id == activityId);
            var activityName = trash.Action.ToString();
            this.db.UserActions.Remove(trash);
            await this.db.SaveChangesAsync();
            return activityName;
        }

        public async Task<ProfileApplicationUserViewModel> ExtractUserInfo(string username, ApplicationUser currentUser)
        {
            var user = await this.db.Users
                .Include(x => x.City)
                .Include(x => x.CountryCode)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.ZipCode)
                .Include(x => x.Comments.Where(y => y.CommentStatus == CommentStatus.Approved))
                .Include(x => x.Posts.Where(y => y.PostStatus == PostStatus.Approved))
                .Include(x => x.PostLikes)
                .Include(x => x.UserActions)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.UserName == username);
            var group = new List<string>() { username, currentUser.UserName };
            var groupName = string.Join(GlobalConstants.ChatGroupNameSeparator, group.OrderBy(x => x));

            var model = this.mapper.Map<ProfileApplicationUserViewModel>(user);
            return model;
        }

        public async Task<ApplicationUser> FollowUser(string username, ApplicationUser currentUser)
        {
            var user = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (!this.db.FollowUnfollows.Any(x => x.ApplicationUserId == user.Id && x.FollowerId == currentUser.Id))
            {
                this.db.FollowUnfollows.Add(new FollowUnfollow
                {
                    ApplicationUserId = user.Id,
                    FollowerId = currentUser.Id,
                    IsFollowed = true,
                });
            }
            else
            {
                this.db.FollowUnfollows.FirstOrDefault(x => x.ApplicationUserId == user.Id && x.FollowerId == currentUser.Id).IsFollowed = true;
            }

            this.AddUserAction(user, UserActionType.Follow, currentUser);
            this.AddUserAction(currentUser, UserActionType.Followed, user);
            await this.db.SaveChangesAsync();

            return currentUser;
        }

        public async Task<ApplicationUser> UnfollowUser(string username, ApplicationUser currentUser)
        {
            var user = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (this.db.FollowUnfollows.Any(x => x.ApplicationUserId == user.Id && x.FollowerId == currentUser.Id && x.IsFollowed == true))
            {
                this.db.FollowUnfollows
                    .FirstOrDefault(x => x.ApplicationUserId == user.Id && x.FollowerId == currentUser.Id && x.IsFollowed == true)
                    .IsFollowed = false;

                this.AddUserAction(user, UserActionType.Unfollow, currentUser);
                this.AddUserAction(currentUser, UserActionType.Unfollowed, user);
                await this.db.SaveChangesAsync();
            }

            return currentUser;
        }

        public async Task<bool> HasAdmin(ApplicationRole role)
        {
            if (role != null)
            {
                var roleId = role.Id;
                return await this.db.UserRoles.AnyAsync(x => x.RoleId == roleId);
            }

            return false;
        }

        public async Task<bool> HasAdministrator()
        {
            var isAdminRoleExist = await this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRole);
            if (isAdminRoleExist == null)
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = GlobalConstants.AdministratorRole,
                    RoleLevel = 1,
                });
            }

            var adminRole = await this.db.Roles
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.AdministratorRole);
            var adminsCount = await this.db.UserRoles
                .CountAsync(x => x.RoleId == adminRole.Id && x.UserId != null);

            return adminsCount != 0;
        }

        public void MakeYourselfAdmin(string username)
        {
            ApplicationUser user = this.db.Users.FirstOrDefault(x => x.UserName == username);
            ApplicationRole role = this.db.Roles.FirstOrDefault(x => x.Name == Roles.Administrator.ToString());

            if (user == null || role == null)
            {
                return;
            }

            if (this.db.UserRoles.Any(x => x.RoleId == role.Id))
            {
                return;
            }

            this.db.UserRoles.Add(new ApplicationUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            this.db.SaveChanges();
        }

        public async Task<double> RateUser(ApplicationUser currentUser, string username, int rate)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var targetRating = await this.db.UserRatings
                .FirstOrDefaultAsync(x => x.Username == username && x.RaterUsername == currentUser.UserName);

            if (targetRating != null)
            {
                targetRating.Stars = rate;
                this.db.Update(targetRating);
            }
            else
            {
                targetRating = new UserRating
                {
                    RaterUsername = currentUser.UserName,
                    Username = username,
                    Stars = rate,
                };
                this.db.UserRatings.Add(targetRating);
            }

            await this.db.SaveChangesAsync();

            if (currentUser.UserName != username)
            {
                string notificationId =
                       await this.notificationRepository
                       .AddProfileRatingNotification(user, currentUser, rate);

                var count = await this.notificationRepository.GetUserNotificationsCount(user.UserName);
                await this.notificationHubContext
                    .Clients
                    .User(user.Id)
                    .SendAsync("ReceiveNotification", count, true);

                var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationId);
                await this.notificationHubContext.Clients.User(user.Id)
                    .SendAsync("VisualizeNotification", notificationForApproving);
            }

            return this.CalculateRatingScore(username);
        }

        public double ExtractUserRatingScore(string username)
        {
            return this.CalculateRatingScore(username);
        }

        public async Task<int> GetLatestScore(ApplicationUser currentUser, string username)
        {
            var target = await this.db.UserRatings
                .FirstOrDefaultAsync(x => x.Username == username && x.RaterUsername == currentUser.UserName);
            return target == null ? 0 : target.Stars;
        }

        public async Task<bool> IsUserExist(string username)
        {
            return await this.db.Users.AnyAsync(x => x.UserName == username);
        }

        public async Task ChangeActionStatus(string username, string id, string newStatus)
        {
            var action = await this.db.UserActions
                .FirstOrDefaultAsync(x => x.Id == id && x.PersonUsername == username);

            if (action != null)
            {
                action.ActionStatus = (UserActionStatus)Enum.Parse(typeof(UserActionStatus), newStatus);
                this.db.UserActions.Update(action);
                await this.db.SaveChangesAsync();
            }
        }

        private double CalculateRatingScore(string username)
        {
            double score;
            var count = this.db.UserRatings.Count(x => x.Username == username);
            if (count != 0)
            {
                var totalScore = this.db.UserRatings.Where(x => x.Username == username).Sum(x => x.Stars);
                score = Math.Round((double)totalScore / count, 2);
                return score;
            }

            return 0;
        }
    }
}
