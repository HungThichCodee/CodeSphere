using CodeSphere.Areas.Administration.ViewModels.EditEmoji.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditEmoji.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.EditEmoji
{
    public interface IEditEmojiRepository
    {
        Task<GetEditEmojiDataViewModel> GetEmojiById(string emojiId);

        ICollection<EditEmojiViewModel> GetAllEmojis();

        Task<Tuple<bool, string>> EditEmoji(EditEmojiInputModel model);
    }
}