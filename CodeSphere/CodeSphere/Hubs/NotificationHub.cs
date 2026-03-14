using CodeSphere.Areas.UserNotifications.Models.Enums;
using CodeSphere.Data;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task GetUserNotificationCount(bool isFirstNotificaitonSound)
        {
            var username = this.Context.User.Identity.Name;
            if (username != null)
            {
                var targetUser = await this.db.Users.FirstOrDefaultAsync(x => x.UserName == username);
                var count = await this.db.UserNotifications
                    .CountAsync(x => x.TargetUsername == username && x.Status == NotificationStatus.Unread);

                await this.Clients.User(targetUser.Id).SendAsync("ReceiveNotification", count, isFirstNotificaitonSound);
            }
        }
    }
}
