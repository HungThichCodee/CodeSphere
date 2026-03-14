using System.Collections;
using System.Collections.Generic;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Areas.PrivateChat.Models.Enums;
using CodeSphere.Areas.PrivateChat.ViewModels.ChatTheme;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.ViewModels.PrivateChat
{
    public class PrivateChatViewModel
    {
        public ApplicationUser? FromUser { get; set; }

        public ApplicationUser? ToUser { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();

        public string? GroupName { get; set; }

        public Dictionary<EmojiType, ICollection<ChatEmojiViewModel>> Emojis { get; set; } =
            new Dictionary<EmojiType, ICollection<ChatEmojiViewModel>>();

        public ChatThemeViewModel ChatThemeViewModel { get; set; } = new ChatThemeViewModel();

        public ICollection<ChatThemeViewModel> AllChatThemes { get; set; } = new HashSet<ChatThemeViewModel>();

        public ICollection<ChatStickerTypeViewModel> AllStickers { get; set; } =
            new HashSet<ChatStickerTypeViewModel>();

        public ICollection<QuickChatReplyViewModel> AllQuickChatReplies { get; set; } =
           new HashSet<QuickChatReplyViewModel>();
    }
}