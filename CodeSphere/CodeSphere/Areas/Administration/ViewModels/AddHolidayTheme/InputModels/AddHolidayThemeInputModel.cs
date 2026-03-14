using System.ComponentModel.DataAnnotations;
using CodeSphere.ApplicationAttributes.ValidationAttributes;

namespace CodeSphere.Areas.Administration.ViewModels.AddHolidayTheme.InputModels
{
    public class AddHolidayThemeInputModel
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        [FilesCollectionRange(1, 14)]
        public ICollection<IFormFile> Icons { get; set; } = new HashSet<IFormFile>();
    }
}
