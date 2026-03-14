using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddEmoji.InputModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddEmoji
{
    public class AddEmojiRepository : IAddEmojiRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddEmojiRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<Tuple<bool, string>> AddEmoji(AddEmojiInputModel model)
        {
            if (this.db.Emojis.Any(x => x.Name.ToUpper() == model.Name.ToUpper() && x.EmojiType == model.EmojiType))
            {
                return Tuple.Create(false, string.Format(ErrorMessages.EmojiAlreadyExist, model.Name.ToUpper()));
            }
            else
            {
                var lastNumber = await this.db.Emojis
                .Where(x => x.EmojiType == model.EmojiType)
                .Select(x => x.Position)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync();
                var emoji = new Emoji
                {
                    EmojiType = model.EmojiType,
                    Name = model.Name,
                    Position = lastNumber + 1,
                };

                var imageUrl = await ApplicationCloudinary.UploadImage(
                    this.cloudinary,
                    model.Image,
                    string.Format(GlobalConstants.EmojiName, emoji.Id),
                    GlobalConstants.EmojisFolder);
                emoji.Url = imageUrl;

                this.db.Emojis.Add(emoji);
                await this.db.SaveChangesAsync();
                return Tuple.Create(true, string.Format(SuccessMessages.SuccessfullyAddedEmoji, emoji.Name));
            }
        }
    }
}
