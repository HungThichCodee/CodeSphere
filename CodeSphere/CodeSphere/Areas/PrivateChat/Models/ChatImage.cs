using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class ChatImage
    {
        public ChatImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [ForeignKey(nameof(Group))]
        [Required]
        public string GroupId { get; set; }

        public Group Group { get; set; }

        [ForeignKey(nameof(ChatMessage))]
        [Required]
        public string ChatMessageId { get; set; }

        public ChatMessage ChatMessage { get; set; }
    }
}
