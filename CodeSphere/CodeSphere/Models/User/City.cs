using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using CodeSphere.Constraints;
using Mono.TextTemplating;

namespace CodeSphere.Models.User
{
    public class City
    {
        public City()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.CityNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(State))]
        public string StateId { get; set; }

        public State State { get; set; }

        [ForeignKey(nameof(Country))]
        public string CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<ZipCode> ZipCodes { get; set; } = new HashSet<ZipCode>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}
