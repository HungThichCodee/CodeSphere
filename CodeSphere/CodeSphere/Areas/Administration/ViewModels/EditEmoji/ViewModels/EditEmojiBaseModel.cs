using CodeSphere.Areas.Administration.ViewModels.EditEmoji.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.EditEmoji.ViewModels
{
    public class EditEmojiBaseModel
    {
        public EditEmojiInputModel EditEmojiInputModel { get; set; } = new EditEmojiInputModel();

        public ICollection<EditEmojiViewModel> EditEmojiViewModel { get; set; } =
            new HashSet<EditEmojiViewModel>();
    }
}