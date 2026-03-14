using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddEmojis.InputModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddEmojis
{
    public class AddEmojisRepository : IAddEmojisRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddEmojisRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<string> AddEmojis(AddEmojisInputModel model)
        {
            var addedEmojisCount = 0;
            var notAddedEmojisCount = 0;

            var lastNumber = await this.db.Emojis
                 .Where(x => x.EmojiType == model.EmojiType)
                 .Select(x => x.Position)
                 .OrderByDescending(x => x)
                 .FirstOrDefaultAsync();

            foreach (var file in model.Images)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                if (this.db.Emojis.Any(x => x.Name.ToUpper() == fileName.ToUpper() && x.EmojiType == model.EmojiType))
                {
                    notAddedEmojisCount++;
                }
                else
                {
                    var emoji = new Emoji
                    {
                        Name = fileName.Length > 120 ? file.ToString().Substring(0, 120) : fileName,
                        Position = lastNumber + 1,
                        EmojiType = model.EmojiType,
                    };

                    var emojiUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        file,
                        string.Format(GlobalConstants.EmojiName, emoji.Id),
                        GlobalConstants.EmojisFolder);
                    emoji.Url = emojiUrl;

                    lastNumber++;
                    addedEmojisCount++;

                    this.db.Emojis.Add(emoji);
                    await this.db.SaveChangesAsync();
                }
            }

            return string.Format(SuccessMessages.SuccessfullyAddedEmojis, addedEmojisCount, notAddedEmojisCount);
        }
    }
}
