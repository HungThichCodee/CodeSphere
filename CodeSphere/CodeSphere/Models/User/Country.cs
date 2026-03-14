using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeSphere.Constraints;
using Mono.TextTemplating;

namespace CodeSphere.Models.User
{
    public class Country
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.CountryNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(CountryCode))]
        public string? CountryCodeId { get; set; }

        public CountryCode? CountryCode { get; set; }

        public ICollection<State> States { get; set; } = new HashSet<State>();

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}
