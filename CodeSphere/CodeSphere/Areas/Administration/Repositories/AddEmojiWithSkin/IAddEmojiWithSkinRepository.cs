using CodeSphere.Areas.Administration.ViewModels.AddEmojiWithSkin.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddEmojiWithSkin
{
    public interface IAddEmojiWithSkinRepository
    {
        Task<string> AddEmojiWithSkin(AddEmojiWithSkinInputModel model);
    }
}
