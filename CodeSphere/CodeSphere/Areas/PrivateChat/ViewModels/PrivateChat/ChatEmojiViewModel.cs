namespace CodeSphere.Areas.PrivateChat.ViewModels.PrivateChat
{
    public class ChatEmojiViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Position { get; set; }

        public ICollection<string> SkinsUrls { get; set; } = new HashSet<string>();
    }
}
