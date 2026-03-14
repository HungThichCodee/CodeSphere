using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.InputModels
{
    public class AddRemoveTagInputModel
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
