using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class StickerType
    {
        public StickerType()
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

        public ICollection<Sticker> Stickers { get; set; } = new HashSet<Sticker>();
    }
}
