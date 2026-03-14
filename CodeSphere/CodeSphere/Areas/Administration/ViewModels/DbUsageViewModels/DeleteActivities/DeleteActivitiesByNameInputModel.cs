using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.DbUsageViewModels.DeleteActivities
{
    public class DeleteActivitiesByNameInputModel
    {
        [Required]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }
    }
}
