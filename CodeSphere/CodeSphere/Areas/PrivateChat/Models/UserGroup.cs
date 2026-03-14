using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.User;

namespace CodeSphere.Areas.PrivateChat.Models
{
    public class UserGroup
    {
        [Required]
        [ForeignKey(nameof(Group))]
        public string GroupId { get; set; }

        public Group Group { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}