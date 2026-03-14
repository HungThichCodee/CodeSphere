using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.UserNotifications.Models.Enums;

namespace CodeSphere.Areas.UserNotifications.ViewModels.ViewModels
{
    public class NotificationViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Heading { get; set; }

        [Required]
        public NotificationStatus Status { get; set; }

        [Required]
        public ICollection<string> AllStatuses { get; set; } = new HashSet<string>();

        [Required]
        public string ImageUrl { get; set; }

        public string TargetFirstName { get; set; }

        public string TargetLastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string TargetUsername { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
