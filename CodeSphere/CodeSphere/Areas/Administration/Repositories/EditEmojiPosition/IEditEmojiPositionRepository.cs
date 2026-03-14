using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.InputModels;
using CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.ViewModels;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.Repositories.EditEmojiPosition
{
    public interface IEditEmojiPositionRepository
    {
        ICollection<EditEmojiPositionViewModel> GetAllEmojisByType(EmojiType emojiType);

        Task<int> EditEmojisPosition(EditEmojiPositionInputModel[] allEmojis);
    }
}