using System.ComponentModel.DataAnnotations;
using CodeSphere.Areas.UserNotifications.Models.Enums;
using CodeSphere.Models.User;
using Ganss.Xss;

namespace CodeSphere.Areas.UserNotifications.ViewModels.InputModels
{
    public class NotificationInputModel
    {
        public string Id { get; set; }

        [Required]
        public NotificationType NotificationType { get; set; }

        [Required]
        public NotificationStatus Status { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(20)]
        public string TargetUsername { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string SanitizedText => new HtmlSanitizer().Sanitize(this.Text);
    }
}
