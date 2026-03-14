using CodeSphere.Areas.Administration.ViewModels.AllEmojis.ViewModels;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.Repositories.AllEmojis
{
    public interface IAllEmojisRepository
    {
        Dictionary<EmojiType, ICollection<EmojiViewModel>> GetAllEmojis();
    }
}
