using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSphere.Models.User
{
    public class ZipCode
    {
        public ZipCode()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public int Code { get; set; }

        [ForeignKey(nameof(City))]
        public string CityId { get; set; }

        public City City { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}
