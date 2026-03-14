using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Areas.PrivateChat.Repositories.PrivateChat;
using CodeSphere.Areas.UserNotifications.Repositories;
using CodeSphere.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Hubs
{
    public class PrivateChatHub : Hub
    {
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly INotificationRepository notificationRepository;
        private readonly IPrivateChatRepository privateChatRepository;
        private readonly ILogger<PrivateChatHub> logger;

        public PrivateChatHub(
            IHubContext<NotificationHub> notificationHubContext,
            INotificationRepository notificationRepository,
            IPrivateChatRepository privateChatRepository,
            ILogger<PrivateChatHub> logger)
        {
            this.notificationHubContext = notificationHubContext;
            this.notificationRepository = notificationRepository;
            this.privateChatRepository = privateChatRepository;
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var username = Context.User?.Identity?.Name;
                logger.LogInformation($"User connected: {username}, ConnectionId: {Context.ConnectionId}");
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in OnConnectedAsync");
                throw;
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var username = Context.User?.Identity?.Name;
                logger.LogInformation($"User disconnected: {username}, ConnectionId: {Context.ConnectionId}");
                if (exception != null)
                {
                    logger.LogError(exception, "Disconnected with error");
                }
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in OnDisconnectedAsync");
            }
        }

        public async Task AddToGroup(string groupName, string toUsername, string fromUsername)
        {
            try
            {
                logger.LogInformation($"AddToGroup - Group: {groupName}, To: {toUsername}, From: {fromUsername}");
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
                await this.privateChatRepository.AddUserToGroup(groupName, toUsername, fromUsername);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in AddToGroup - Group: {groupName}, To: {toUsername}, From: {fromUsername}");
                throw;
            }
        }

        public async Task SendMessage(string fromUsername, string toUsername, string message, string group)
        {
            try
            {
                logger.LogInformation($"SendMessage - From: {fromUsername}, To: {toUsername}, Group: {group}");
                
                string toId =
                    await this.privateChatRepository.SendMessageToUser(fromUsername, toUsername, message, group);
                string notificationId =
                    await this.notificationRepository.AddMessageNotification(fromUsername, toUsername, message, group);

                var count = await this.notificationRepository.GetUserNotificationsCount(toUsername);
                await this.notificationHubContext.Clients.User(toId).SendAsync("ReceiveNotification", count, true);

                var notification = await this.notificationRepository.GetNotificationById(notificationId);
                await this.notificationHubContext.Clients.User(toId)
                    .SendAsync("VisualizeNotification", notification);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SendMessage - From: {fromUsername}, To: {toUsername}");
                throw;
            }
        }

        public async Task SendStickerMessage(string fromUsername, string toUsername, string group, string stickerUrl)
        {
            try
            {
                logger.LogInformation($"SendStickerMessage - From: {fromUsername}, To: {toUsername}");
                await this.privateChatRepository.SendStickerMessageToUser(
                    fromUsername,
                    toUsername,
                    group,
                    stickerUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SendStickerMessage - From: {fromUsername}, To: {toUsername}");
                throw;
            }
        }

        public async Task ReceiveMessage(string fromUsername, string message, string group)
        {
            try
            {
                logger.LogInformation($"ReceiveMessage - From: {fromUsername}, Group: {group}");
                await this.privateChatRepository.ReceiveNewMessage(fromUsername, message, group);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in ReceiveMessage - From: {fromUsername}");
                throw;
            }
        }

        public async Task ReceiveStickerMessage(string fromUsername, string group, string stickerUrl)
        {
            try
            {
                logger.LogInformation($"ReceiveStickerMessage - From: {fromUsername}, Group: {group}");
                await this.privateChatRepository.ReceiveStickerMessage(fromUsername, group, stickerUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in ReceiveStickerMessage - From: {fromUsername}");
                throw;
            }
        }

        public async Task UpdateMessageNotifications(string fromUsername, string username)
        {
            try
            {
                logger.LogInformation($"UpdateMessageNotifications - From: {fromUsername}, User: {username}");
                var toId = await this.notificationRepository.UpdateMessageNotifications(fromUsername, username);
                if (toId != string.Empty)
                {
                    var count = await this.notificationRepository.GetUserNotificationsCount(username);
                    await this.notificationHubContext
                        .Clients
                        .User(toId)
                        .SendAsync("ReceiveNotification", count, false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UpdateMessageNotifications - From: {fromUsername}, User: {username}");
                throw;
            }
        }

        public async Task UserType(string fromUsername, string toUsername, string fromUserImageUrl)
        {
            try
            {
                logger.LogInformation($"UserType - From: {fromUsername}, To: {toUsername}");
                await this.privateChatRepository.UserType(fromUsername, toUsername, fromUserImageUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UserType - From: {fromUsername}, To: {toUsername}");
                throw;
            }
        }

        public async Task UserStopType(string toUsername)
        {
            try
            {
                logger.LogInformation($"UserStopType - To: {toUsername}");
                await this.privateChatRepository.UserStopType(toUsername);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UserStopType - To: {toUsername}");
                throw;
            }
        }
    }
}
