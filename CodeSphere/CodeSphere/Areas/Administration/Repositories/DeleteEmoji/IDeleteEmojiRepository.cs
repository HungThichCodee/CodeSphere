using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.InputModels;
using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.DeleteEmoji
{
   public interface IDeleteEmojiRepository
    {
        ICollection<DeleteEmojiViewModel> GetAllEmojis();

        Task<Tuple<bool, string>> DeleteEmoji(DeleteEmojiInputModel model);

        Task<string> GetEmojiUrl(string emojiId);
    }
}