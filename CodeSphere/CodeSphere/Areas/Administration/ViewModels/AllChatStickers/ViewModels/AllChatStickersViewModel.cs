namespace CodeSphere.Areas.Administration.ViewModels.AllChatStickers.ViewModels
{
    public class AllChatStickersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public string Url { get; set; }

        public ICollection<AllChatStickersStickerViewModel> AllStickerst { get; set; } =
            new HashSet<AllChatStickersStickerViewModel>();
    }
}