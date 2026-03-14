using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;

namespace CodeSphere.Models.User
{
    public class UserAction
    {
        public UserAction()
        {
            this.ActionStatus = UserActionStatus.Unread;
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [EnumDataType(typeof(UserActionType))]
        public UserActionType Action { get; set; }

        [Required]
        public DateTime ActionDate { get; set; }

        public string? PersonUsername { get; set; }

        public string? FollowerUsername { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? CoverImageUrl { get; set; }

        [ForeignKey(nameof(Post))]
        public string? PostId { get; set; }

        public Post Post { get; set; }

        [MaxLength(150)]
        public string? PostTitle { get; set; }

        [MaxLength(350)]
        public string? PostContent { get; set; }

        [Required]
        public UserActionStatus ActionStatus { get; set; }
    }
}
