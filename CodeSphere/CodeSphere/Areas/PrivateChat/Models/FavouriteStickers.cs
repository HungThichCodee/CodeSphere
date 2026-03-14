using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class FavouriteStickers
    {
        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey(nameof(StickerType))]
        public string StickerTypeId { get; set; }

        public StickerType StickerType { get; set; }

        [Required]
        public bool IsFavourite { get; set; }
    }
}
