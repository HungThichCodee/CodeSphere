using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DashboardViewModels
{
    public class RemoveUserFromRoleInputModel
    {
        [Required]
        public string? Role { get; set; }

        [Required]
        public string? Username { get; set; }
    }
}
