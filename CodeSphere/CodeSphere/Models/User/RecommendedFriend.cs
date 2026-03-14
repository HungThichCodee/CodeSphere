using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Models.User
{
    public class RecommendedFriend
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public string? RecommendedApplicationUserId { get; set; }

        public ApplicationUser? RecommendedApplicationUser { get; set; }
    }
}
