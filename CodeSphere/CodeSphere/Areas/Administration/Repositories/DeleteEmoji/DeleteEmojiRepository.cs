using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.ViewModels;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.DeleteEmoji
{
    public class DeleteEmojiRepository : IDeleteEmojiRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public DeleteEmojiRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> DeleteEmoji(DeleteEmojiInputModel model)
        {
            var emoji = await this.db.Emojis.FirstOrDefaultAsync(x => x.Id == model.Id);
            var emojiType = emoji.EmojiType;

            if (emoji != null)
            {
                string emojiName = emoji.Name;

                var emojiSkins = this.db.EmojiSkins.Where(x => x.EmojiId == emoji.Id).ToList();
                foreach (var emojiSkin in emojiSkins)
                {
                    ApplicationCloudinary.DeleteImage(
                       this.cloudinary,
                       string.Format(GlobalConstants.EmojiSkin, emojiSkin.Id),
                       GlobalConstants.EmojisFolder);
                }

                ApplicationCloudinary.DeleteImage(
                    this.cloudinary,
                    string.Format(GlobalConstants.EmojiName, emoji.Id),
                    GlobalConstants.EmojisFolder);
                this.db.EmojiSkins.RemoveRange(emojiSkins);
                this.db.Emojis.Remove(emoji);
                this.db.SaveChanges();

                var targetToUpdate = this.db.Emojis
                    .Where(x => x.EmojiType == emojiType)
                    .OrderBy(x => x.Position)
                    .ToList();

                for (int i = 0; i < targetToUpdate.Count(); i++)
                {
                    targetToUpdate[i].Position = i + 1;
                }

                this.db.Emojis.UpdateRange(targetToUpdate);
                await this.db.SaveChangesAsync();

                return Tuple.Create(
                    true,
                    string.Format(SuccessMessages.SuccessfullyDeleteEmoji, emojiName.ToUpper()));
            }
            else
            {
                return Tuple.Create(false, ErrorMessages.EmojiDoesNotExist);
            }
        }

        public ICollection<DeleteEmojiViewModel> GetAllEmojis()
        {
            var emojis = this.db.Emojis.OrderBy(x => x.EmojiType).ThenBy(x => x.Position).ToList();
            var result = new List<DeleteEmojiViewModel>();
            foreach (var emoji in emojis)
            {
                result.Add(new DeleteEmojiViewModel
                {
                    Id = emoji.Id,
                    Name = emoji.Name,
                    Url = emoji.Url,
                    SkinsUrls = this.db.EmojiSkins.Where(x => x.EmojiId == emoji.Id).OrderBy(x => x.Position).Select(x => x.Url).ToList(),
                });
            }

            return result;
        }

        public async Task<string> GetEmojiUrl(string emojiId)
        {
            var emoji = await this.db.Emojis.FirstOrDefaultAsync(x => x.Id == emojiId);
            return emoji.Url;
        }
    }
}