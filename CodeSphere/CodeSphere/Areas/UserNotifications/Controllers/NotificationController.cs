using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Constraints;
using CodeSphere.Hubs;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CodeSphere.Areas.UserNotifications.Controllers
{
    [Area(GlobalConstants.NotificationsArea)]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationRepository notificationRepository;
        private readonly IHubContext<NotificationHub> hubContext;

        public NotificationController(
            UserManager<ApplicationUser> userManager,
            INotificationRepository notificationRepository,
            IHubContext<NotificationHub> hubContext)
        {
            this.userManager = userManager;
            this.notificationRepository = notificationRepository;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var result = await this.notificationRepository
                .GetUserNotifications(currentUser, GlobalConstants.NotificationOnClick, 0);

            return this.View(result);
        }

        [HttpPost]
        [Route("/UserNotifications/Notification/EditStatus")]
        public async Task<bool> EditStatus(string newStatus, string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isEdited = await this.notificationRepository.EditStatus(currentUser, newStatus, id);
            await this.ChangeNotificationCounter(isEdited, currentUser);
            return isEdited;
        }

        [HttpPost]
        [Route("/UserNotifications/Notification/DeleteNotification")]
        public async Task<bool> DeleteNotification(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isDeleted = await this.notificationRepository.DeleteNotification(currentUser.UserName, id);
            await this.ChangeNotificationCounter(isDeleted, currentUser);
            return isDeleted;
        }

        [HttpGet]
        [Route("/UserNotifications/Notification/GetMoreNotitification")]
        public async Task<IActionResult> GetMoreNotitification(int skip, int take)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var result = await this.notificationRepository.GetUserNotifications(currentUser, take, skip);
            return new JsonResult(new { newNotifications = result.Item1, hasMore = result.Item2 });
        }

        private async Task ChangeNotificationCounter(bool isForChange, ApplicationUser user)
        {
            if (isForChange)
            {
                int count = await this.notificationRepository.GetUserNotificationsCount(user.UserName);
                await this.hubContext.Clients.User(user.Id).SendAsync("ReceiveNotification", count, false);
            }
        }
    }
}
