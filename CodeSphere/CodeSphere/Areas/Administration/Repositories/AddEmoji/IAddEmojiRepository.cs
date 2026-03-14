using CodeSphere.Areas.Administration.ViewModels.AddEmoji.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddEmoji
{
    public interface IAddEmojiRepository
    {
        Task<Tuple<bool, string>> AddEmoji(AddEmojiInputModel model);
    }
}
