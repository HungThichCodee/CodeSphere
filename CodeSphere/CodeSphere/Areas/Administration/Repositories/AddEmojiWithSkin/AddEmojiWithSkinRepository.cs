using CloudinaryDotNet;
using CodeSphere.Areas.Administration.ViewModels.AddEmojiWithSkin.InputModels;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Repositories.CloudRepositories;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Administration.Repositories.AddEmojiWithSkin
{
    public class AddEmojiWithSkinRepository : IAddEmojiWithSkinRepository
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public AddEmojiWithSkinRepository(ApplicationDbContext db, Cloudinary cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<string> AddEmojiWithSkin(AddEmojiWithSkinInputModel model)
        {
            var targetEmoji = await this.db.Emojis.FirstOrDefaultAsync(x => x.Name.ToUpper() == model.Name.ToUpper() && x.EmojiType == model.EmojiType);
            var addedSkins = 0;

            if (targetEmoji == null)
            {
                var lastEmojiNumber = await this.db.Emojis
                   .Where(x => x.EmojiType == model.EmojiType)
                   .Select(x => x.Position)
                   .OrderByDescending(x => x)
                   .FirstOrDefaultAsync();

                targetEmoji = new Emoji
                {
                    Name = model.Name,
                    EmojiType = model.EmojiType,
                    Position = lastEmojiNumber + 1,
                };

                var imgeUrl = await ApplicationCloudinary.UploadImage(
                    this.cloudinary,
                    model.Image,
                    string.Format(GlobalConstants.EmojiName, targetEmoji.Id),
                    GlobalConstants.EmojisFolder);
                targetEmoji.Url = imgeUrl;

                var lastEmojiSkinNumber = await this.db.EmojiSkins
                   .Where(x => x.EmojiId == targetEmoji.Id)
                   .Select(x => x.Position)
                   .OrderByDescending(x => x)
                   .FirstOrDefaultAsync();
                foreach (var skinFile in model.ImageSkins)
                {
                    var fileName = Path.GetFileNameWithoutExtension(skinFile.FileName);
                    var emojiSkin = new EmojiSkin
                    {
                        EmojiId = targetEmoji.Id,
                        Name = fileName,
                        Position = lastEmojiSkinNumber + 1,
                    };

                    var skinUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        skinFile,
                        string.Format(GlobalConstants.EmojiSkin, emojiSkin.Id),
                        GlobalConstants.EmojisFolder);
                    emojiSkin.Url = skinUrl;

                    this.db.Emojis.Add(targetEmoji);
                    this.db.EmojiSkins.Add(emojiSkin);
                    lastEmojiSkinNumber++;
                    addedSkins++;
                }

                await this.db.SaveChangesAsync();
                return string.Format(
                    SuccessMessages.SuccessfullyAddedEmojiWithSkins,
                    model.Name.ToUpper(),
                    addedSkins);
            }
            else
            {
                var targetSkins = this.db.EmojiSkins.Where(x => x.EmojiId == targetEmoji.Id).ToList();

                var lastEmojiSkinNumber = await this.db.EmojiSkins
                    .Where(x => x.EmojiId == targetEmoji.Id)
                    .Select(x => x.Position)
                    .OrderByDescending(x => x)
                    .FirstOrDefaultAsync();
                foreach (var skinFile in model.ImageSkins)
                {
                    var fileName = Path.GetFileNameWithoutExtension(skinFile.FileName);
                    if (!targetSkins.Any(x => x.Name.ToUpper() == fileName.ToUpper()))
                    {
                        var emojiSkin = new EmojiSkin
                        {
                            EmojiId = targetEmoji.Id,
                            Name = fileName,
                            Position = lastEmojiSkinNumber + 1,
                        };

                        var skinUrl = await ApplicationCloudinary.UploadImage(
                        this.cloudinary,
                        skinFile,
                        string.Format(GlobalConstants.EmojiSkin, emojiSkin.Id),
                        GlobalConstants.EmojisFolder);
                        emojiSkin.Url = skinUrl;

                        targetSkins.Add(emojiSkin);
                        lastEmojiSkinNumber++;
                        addedSkins++;
                        this.db.EmojiSkins.Add(emojiSkin);
                    }
                }

                await this.db.SaveChangesAsync();
                return string.Format(
                    SuccessMessages.SuccessfullyAddedEmojiWithSkins,
                    model.Name.ToUpper(),
                    addedSkins);
            }
        }
    }
}
