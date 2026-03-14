using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class Sticker
    {
        public Sticker()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        [ForeignKey(nameof(StickerType))]
        public string StickerTypeId { get; set; }

        public StickerType StickerType { get; set; }
    }
}
