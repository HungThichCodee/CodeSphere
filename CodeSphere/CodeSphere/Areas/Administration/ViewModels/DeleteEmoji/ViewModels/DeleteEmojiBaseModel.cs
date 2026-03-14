using CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.DeleteEmoji.ViewModels
{
    public class DeleteEmojiBaseModel
    {
        public DeleteEmojiInputModel DeleteEmojiInputModel { get; set; } = new DeleteEmojiInputModel();

        public ICollection<DeleteEmojiViewModel> DeleteEmojiViewModels { get; set; } =
            new HashSet<DeleteEmojiViewModel>();
    }
}