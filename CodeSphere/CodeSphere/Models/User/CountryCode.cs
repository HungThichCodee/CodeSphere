using System.ComponentModel.DataAnnotations;
using CodeSphere.Constraints;

namespace CodeSphere.Models.User
{
    public class CountryCode
    {
        public CountryCode()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstraints.CountryCodeMaxLength)]
        public string Code { get; set; }

        public ICollection<Country> Coutries { get; set; } = new HashSet<Country>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}
