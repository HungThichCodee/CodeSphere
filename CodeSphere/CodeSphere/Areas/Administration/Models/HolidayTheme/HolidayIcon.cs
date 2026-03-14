using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Areas.Administration.Models.HolidayTheme
{
    public class HolidayIcon
    {
        public HolidayIcon()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [ForeignKey(nameof(HolidayTheme))]
        public string HolidayThemeId { get; set; }

        public HolidayTheme HolidayTheme { get; set; }
    }
}
