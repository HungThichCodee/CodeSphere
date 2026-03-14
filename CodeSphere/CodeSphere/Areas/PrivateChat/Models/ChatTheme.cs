using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class ChatTheme
    {
        public ChatTheme()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    }
}
