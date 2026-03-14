using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.EditEmoji.ViewModels
{
    public class GetEditEmojiDataViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public EmojiType EmojiType { get; set; }
    }
}