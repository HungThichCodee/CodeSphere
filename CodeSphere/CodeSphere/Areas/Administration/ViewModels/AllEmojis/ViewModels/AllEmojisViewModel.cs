

using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.AllEmojis.ViewModels
{
    public class AllEmojisViewModel
    {
        public Dictionary<EmojiType, ICollection<EmojiViewModel>> AllEmojisViewModels { get; set; } =
            new Dictionary<EmojiType, ICollection<EmojiViewModel>>();
    }
}