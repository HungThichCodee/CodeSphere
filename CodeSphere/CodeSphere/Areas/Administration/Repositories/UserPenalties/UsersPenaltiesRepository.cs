using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Hubs;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.UserPenalties
{
    public class UsersPenaltiesRepository : IUsersPenaltiesRepository
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly INotificationRepository notificationRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersPenaltiesRepository(
            ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            IHubContext<NotificationHub> notificationHubContext,
            INotificationRepository notificationRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.notificationHubContext = notificationHubContext;
            this.notificationRepository = notificationRepository;
            this.userManager = userManager;
        }

        public async Task<bool> BlockUser(string username, ApplicationUser currentUser, string reasonToBeBlocked)
        {
            var user = this.db.Users.FirstOrDefault(x => x.UserName == username);
            var adminRole = await this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRole);

            if (this.db.UserRoles.Any(x => x.RoleId == adminRole.Id && x.UserId == user.Id))
            {
                return false;
            }

            if (user != null && user.IsBlocked == false)
            {
                user.IsBlocked = true;
                user.ReasonToBeBlocked = reasonToBeBlocked ?? "Your profile was banned for some reason. Contact with administrators for more information.";

                var targetUser = await this.db.Users
                        .FirstOrDefaultAsync(x => x.Id == user.Id);
                var message = reasonToBeBlocked ?? "Your profile was banned for some reason. Contact with administrators for more information.";

                string notificationId =
                       await this.notificationRepository
                       .AddBannUserNotification(targetUser, currentUser, message);

                var count = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                await this.notificationHubContext
                    .Clients
                    .User(targetUser.Id)
                    .SendAsync("ReceiveNotification", count, true);

                var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationId);
                await this.notificationHubContext.Clients.User(targetUser.Id)
                    .SendAsync("VisualizeNotification", notificationForApproving);

                this.db.Users.Update(user);
                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UnblockUser(string username, ApplicationUser currentUser)
        {
            var user = this.db.Users.FirstOrDefault(x => x.UserName == username);

            if (user != null && user.IsBlocked == true)
            {
                user.IsBlocked = false;
                user.ReasonToBeBlocked = string.Empty;

                var targetUser = await this.db.Users
                        .FirstOrDefaultAsync(x => x.Id == user.Id);
                var message = "Your profile was unbanned. Please follow the website rules!!!";

                string notificationId =
                       await this.notificationRepository
                       .AddUnbannUserNotification(targetUser, currentUser, message);

                var count = await this.notificationRepository.GetUserNotificationsCount(targetUser.UserName);
                await this.notificationHubContext
                    .Clients
                    .User(targetUser.Id)
                    .SendAsync("ReceiveNotification", count, true);

                var notificationForApproving = await this.notificationRepository.GetNotificationById(notificationId);
                await this.notificationHubContext.Clients.User(targetUser.Id)
                    .SendAsync("VisualizeNotification", notificationForApproving);

                this.db.Users.Update(user);
                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public ICollection<string> GetAllBlockedUsers()
        {
            return this.db.Users.Where(u => u.IsBlocked == true).Select(x => x.UserName).ToList();
        }

        public async Task<ICollection<string>> GetAllNotBlockedUsers()
        {
            var adminRole = await this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRole);
            var usersAdminsIds = this.db.UserRoles
                .Where(x => x.RoleId == adminRole.Id)
                .Select(x => x.UserId)
                .ToList();
            return this.db.Users
                .Where(u => u.IsBlocked == false && !usersAdminsIds.Contains(u.Id))
                .Select(x => x.UserName)
                .ToList();
        }

        public async Task<int> BlockAllUsers()
        {
            var users = this.db.Users.Where(x => x.IsBlocked == false).ToList();
            int count = 0;

            foreach (var user in users)
            {
                if (!await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRole))
                {
                    count++;
                    user.IsBlocked = true;
                }
            }

            this.db.Users.UpdateRange(users);
            await this.db.SaveChangesAsync();
            return count;
        }

        public async Task<int> UnblockAllUsers()
        {
            var users = this.db.Users.Where(x => x.IsBlocked == true).ToList();
            int count = users.Count();

            foreach (var user in users)
            {
                user.IsBlocked = false;
            }

            this.db.Users.UpdateRange(users);
            await this.db.SaveChangesAsync();
            return count;
        }
    }
}
