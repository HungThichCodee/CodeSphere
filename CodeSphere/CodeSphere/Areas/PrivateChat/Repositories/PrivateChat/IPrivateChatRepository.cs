using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Areas.PrivateChat.Models.Enums;
using CodeSphere.Areas.PrivateChat.ViewModels.ChatTheme;
using CodeSphere.Areas.PrivateChat.ViewModels.PrivateChat;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.Repositories.PrivateChat
{
    public interface IPrivateChatRepository
    {
        Task<ICollection<ChatMessage>> ExtractAllMessages(string group);

        Task<bool> IsUserAbleToChat(string username, string group, ApplicationUser user);

        Task AddUserToGroup(string groupName, string toUsername, string fromUsername);

        Task<string> SendMessageToUser(string fromUsername, string toUsername, string message, string group);

        Task ReceiveNewMessage(string fromUsername, string message, string group);

        Dictionary<EmojiType, ICollection<ChatEmojiViewModel>> GetAllEmojis();

        ICollection<ChatThemeViewModel> GetAllThemes();

        ChatThemeViewModel GetGroupTheme(string group);

        Task ChangeChatTheme(string username, string group, string themeId);

        Task<SendFilesResponseViewModel> SendMessageWitFilesToUser(IList<IFormFile> files, string group, string toUsername, string fromUsername, string message);

        Task UserType(string fromUsername, string toUsername, string fromUserImageUrl);

        Task UserStopType(string toUsername);

        ICollection<ChatStickerTypeViewModel> GetAllStickers(ApplicationUser currentUser);

        Task SendStickerMessageToUser(string fromUsername, string toUsername, string group, string stickerUrl);

        Task ReceiveStickerMessage(string fromUsername, string group, string stickerUrl);

        Task<ICollection<LoadMoreMessagesViewModel>> LoadMoreMessages(string group, int messagesSkipCount, ApplicationUser currentUser);

        ICollection<QuickChatReplyViewModel> GetAllQuickReplies(ApplicationUser currentUser);

        Task<QuickChatReplyViewModel> AddQuickChatReply(ApplicationUser currentUser, string quickReplyText);

        Task<Tuple<bool, string>> RemoveQuickChatReply(ApplicationUser currentUser, string id);
    }
}