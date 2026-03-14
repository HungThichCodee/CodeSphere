using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.ViewModels
{
    public class EditEmojiPositionViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Position { get; set; }

        public EmojiType EmojiType { get; set; }
    }
}