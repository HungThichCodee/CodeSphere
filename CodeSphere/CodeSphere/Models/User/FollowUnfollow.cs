using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Models.User
{
    public class FollowUnfollow
    {
        [Required]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public string? FollowerId { get; set; }

        public ApplicationUser? Follower { get; set; }

        [Required]
        public bool IsFollowed { get; set; }
    }
}
