using CodeSphere.Areas.Administration.Repositories.EditEmojiPosition;
using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.ViewModels;
using CodeSphere.Areas.PrivateChat.Models.Enums;
using CodeSphere.Data;
using Microsoft.EntityFrameworkCore;

namespace SdvCode.Areas.Administration.Services.EditEmojiPosition
{
    public class EditEmojiPositionRepository : IEditEmojiPositionRepository
    {
        private readonly ApplicationDbContext db;

        public EditEmojiPositionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> EditEmojisPosition(EditEmojiPositionInputModel[] allEmojis)
        {
            var count = 0;
            foreach (var emoji in allEmojis)
            {
                var targetEmoji = await this.db.Emojis
                    .FirstOrDefaultAsync(x => x.Id == emoji.Id && x.Name == emoji.Name);
                if (targetEmoji.Position != emoji.Position)
                {
                    count++;
                    targetEmoji.Position = emoji.Position;
                    this.db.Emojis.Update(targetEmoji);
                }
            }

            await this.db.SaveChangesAsync();
            return count;
        }

        public ICollection<EditEmojiPositionViewModel> GetAllEmojisByType(EmojiType emojiType)
        {
            var emojis = this.db.Emojis.Where(x => x.EmojiType == emojiType).OrderBy(x => x.Position).ToList();
            var result = new List<EditEmojiPositionViewModel>();

            foreach (var emoji in emojis)
            {
                result.Add(new EditEmojiPositionViewModel
                {
                    Id = emoji.Id,
                    Name = emoji.Name,
                    Url = emoji.Url,
                    Position = emoji.Position,
                    EmojiType = emoji.EmojiType,
                });
            }

            return result;
        }
    }
}