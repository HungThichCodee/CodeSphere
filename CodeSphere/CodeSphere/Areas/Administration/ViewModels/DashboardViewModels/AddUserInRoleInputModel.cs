using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DashboardViewModels
{
    public class AddUserInRoleInputModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
