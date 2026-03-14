using System.ComponentModel.DataAnnotations;
using CodeSphere.Constraints;
using Microsoft.AspNetCore.Identity;

namespace CodeSphere.Models.User
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        public int RoleLevel { get; set; }

        [MaxLength(GlobalConstants.RoldeDescriptionMaxLength)]
        public string? Description { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
