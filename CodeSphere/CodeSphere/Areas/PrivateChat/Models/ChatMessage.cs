using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class ChatMessage
    {
        public ChatMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey(nameof(Group))]
        public string GroupId { get; set; }

        public Group Group { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ReceiverUsername { get; set; }

        public string? RecieverImageUrl { get; set; }

        [Required]
        public DateTime SendedOn { get; set; }

        public ICollection<ChatImage> ChatImages { get; set; } = new HashSet<ChatImage>();
    }
}