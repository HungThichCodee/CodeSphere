using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DashboardViewModels
{
    public class CreateRoleInputModel
    {
        [Required]
        public string? Role { get; set; }
    }
}
