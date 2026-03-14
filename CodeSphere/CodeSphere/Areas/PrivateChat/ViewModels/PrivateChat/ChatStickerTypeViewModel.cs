namespace CodeSphere.Areas.PrivateChat.ViewModels.PrivateChat
{
    public class ChatStickerTypeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public string Url { get; set; }

        public ICollection<ChatStickerViewModel> Stickers { get; set; } = new HashSet<ChatStickerViewModel>();
    }
}
