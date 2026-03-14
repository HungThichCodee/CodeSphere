using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.UsersPenalties
{
    public class UsersPenaltiesInputModel
    {
        [Required]
        public string Username { get; set; }

        [MaxLength(200)]
        [Display(Name = "Reason To Be Blocked")]
        public string ReasonToBeBlocked { get; set; }
    }
}
