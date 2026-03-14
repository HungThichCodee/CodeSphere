using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.Administration.Models.HolidayTheme
{
    public class HolidayTheme
    {
        public HolidayTheme()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = false;
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<HolidayIcon> HolidayIcons { get; set; } = new HashSet<HolidayIcon>();
    }
}
