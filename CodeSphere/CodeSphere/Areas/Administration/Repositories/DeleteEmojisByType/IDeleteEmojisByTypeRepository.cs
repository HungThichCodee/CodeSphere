using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.Repositories.DeleteEmojisByType
{
    public interface IDeleteEmojisByTypeRepository
    {
        Task<string> DeleteEmojisByType(EmojiType emojiType);
    }
}