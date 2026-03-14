namespace CodeSphere.Areas.PrivateChat.ViewModels.CollectStickers.ViewModels
{
    public class CollectStickersBaseModel
    {
        public IEnumerable<CollectStickersStickerTypeViewModel> AllStickerTypes { get; set; } =
           new HashSet<CollectStickersStickerTypeViewModel>();
    }
}
