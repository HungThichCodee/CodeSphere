using System.ComponentModel.DataAnnotations;

namespace CodeSphere.ViewModels.Users.InputModels
{
    public class ExternalLoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
