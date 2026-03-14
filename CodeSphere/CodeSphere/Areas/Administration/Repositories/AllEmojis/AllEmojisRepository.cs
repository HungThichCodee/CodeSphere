using CodeSphere.Areas.Administration.ViewModels.AllEmojis.ViewModels;
using CodeSphere.Areas.PrivateChat.Models.Enums;
using CodeSphere.Data;

namespace CodeSphere.Areas.Administration.Repositories.AllEmojis
{
    public class AllEmojisRepository : IAllEmojisRepository
    {
        private readonly ApplicationDbContext db;

        public AllEmojisRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Dictionary<EmojiType, ICollection<EmojiViewModel>> GetAllEmojis()
        {
            var result = new Dictionary<EmojiType, ICollection<EmojiViewModel>>();

            foreach (var emojiType in Enum.GetValues(typeof(EmojiType)))
            {
                result.Add((EmojiType)emojiType, new List<EmojiViewModel>());
                var emojis = this.db.Emojis
                    .Where(x => x.EmojiType == (EmojiType)emojiType)
                    .OrderBy(x => x.Position)
                    .ToList();

                foreach (var emoji in emojis)
                {
                    result[(EmojiType)emojiType].Add(new EmojiViewModel
                    {
                        Id = emoji.Id,
                        Name = emoji.Name,
                        Position = emoji.Position,
                        Url = emoji.Url,
                        EmojiType = emoji.EmojiType,
                        SkinsUrls = this.db.EmojiSkins.Where(x => x.EmojiId == emoji.Id).OrderBy(x => x.Position).Select(x => x.Url).ToList(),
                    });
                }
            }

            return result;
        }
    }
}
