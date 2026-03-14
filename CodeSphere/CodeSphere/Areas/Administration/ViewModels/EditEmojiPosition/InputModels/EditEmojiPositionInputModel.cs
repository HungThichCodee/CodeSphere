using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.EditEmojiPosition.InputModels
{
    public class EditEmojiPositionInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public int Position { get; set; }
    }
}