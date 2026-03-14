namespace CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.ViewModels
{
    public class DeleteEmojiViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<string> SkinsUrls { get; set; } = new HashSet<string>();
    }
}