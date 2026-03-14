namespace CodeSphere.Areas.PrivateChat.ViewModels.CollectStickers.ViewModels
{
    public class CollectStickersStickerTypeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool HaveIt { get; set; }

        public ICollection<CollectStickersStickerViewModel> AllStickers { get; set; } =
            new HashSet<CollectStickersStickerViewModel>();
    }
}
