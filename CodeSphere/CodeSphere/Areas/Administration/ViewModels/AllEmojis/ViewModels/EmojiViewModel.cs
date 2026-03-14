using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.AllEmojis.ViewModels
{
    public class EmojiViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Position { get; set; }

        public EmojiType EmojiType { get; set; }

        public ICollection<string> SkinsUrls { get; set; } = new HashSet<string>();
    }
}