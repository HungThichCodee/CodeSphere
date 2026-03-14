using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DbUsageViewModels.DeleteUsersImages
{
    public class DeleteImagesByUsernameInputModel
    {
        [Required]
        public string? Username { get; set; }
    }
}
