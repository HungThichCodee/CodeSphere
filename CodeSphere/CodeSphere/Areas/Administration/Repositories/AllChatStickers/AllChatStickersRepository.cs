using CodeSphere.Areas.Administration.ViewModels.AllChatStickers.ViewModels;
using CodeSphere.Data;

namespace CodeSphere.Areas.Administration.Repositories.AllChatStickers
{
    public class AllChatStickersRepository : IAllChatStickersRepository
    {
        private readonly ApplicationDbContext db;

        public AllChatStickersRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AllChatStickersViewModel> GetAllChatStickers()
        {
            var result = new List<AllChatStickersViewModel>();

            var allStickersTypes = this.db.StickerTypes
                .OrderBy(x => x.Position)
                .ThenBy(x => x.Name)
                .ToList();

            foreach (var stickerType in allStickersTypes)
            {
                var targetType = new AllChatStickersViewModel
                {
                    Id = stickerType.Id,
                    Name = stickerType.Name,
                    Position = stickerType.Position,
                    Url = stickerType.Url,
                };

                var allStickers = this.db.Stickers
                    .Where(x => x.StickerTypeId == stickerType.Id)
                    .OrderBy(x => x.Position)
                    .ThenBy(x => x.Name)
                    .ToList();

                foreach (var sticker in allStickers)
                {
                    targetType.AllStickerst.Add(new AllChatStickersStickerViewModel
                    {
                        Id = sticker.Id,
                        Name = sticker.Name,
                        Position = sticker.Position,
                        Url = sticker.Url,
                    });
                }

                result.Add(targetType);
            }

            return result;
        }
    }
}

