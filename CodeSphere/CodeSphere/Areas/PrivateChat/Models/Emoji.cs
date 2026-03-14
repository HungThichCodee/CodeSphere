using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.PrivateChat.Models.Enums;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class Emoji
    {
        public Emoji()
        {
            this.Id = Guid.NewGuid().ToString();
        }

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
        [EnumDataType(typeof(EmojiType))]
        public EmojiType EmojiType { get; set; }

        public ICollection<EmojiSkin> EmojiSkins { get; set; } = new HashSet<EmojiSkin>();
    }
}
