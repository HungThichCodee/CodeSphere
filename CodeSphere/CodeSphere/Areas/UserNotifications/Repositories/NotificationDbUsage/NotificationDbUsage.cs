using CodeSphere.Areas.UserNotifications.Models.Enums;
using CodeSphere.Data;

namespace CodeSphere.Areas.UserNotifications.Repositories.NotificationDbUsage
{
    public class NotificationDbUsage : INotificationDbUsage
    {
        private readonly ApplicationDbContext db;

        public NotificationDbUsage(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task DeleteNotifications()
        {
            var target = this.db.UserNotifications
                .Where(x => x.Status == NotificationStatus.Read)
                .ToList();
            this.db.UserNotifications.RemoveRange(target);
            await this.db.SaveChangesAsync();
        }
    }
}
