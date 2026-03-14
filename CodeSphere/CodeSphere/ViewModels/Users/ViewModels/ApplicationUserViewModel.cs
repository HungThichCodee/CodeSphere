using CodeSphere.Models.Enums;
using CodeSphere.Models.User;

namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string ZipCodeId { get; set; }

        public ZipCodeViewModel ZipCode { get; set; }

        public string CountryId { get; set; }

        public CountryViewModel Country { get; set; }

        public string StateId { get; set; }

        public StateViewModel State { get; set; }

        public string CityId { get; set; }

        public CityViewModel City { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegisteredOn { get; set; }

        public Gender Gender { get; set; }

        public string CountryCodeId { get; set; }

        public CountryCodeViewModel CountryCode { get; set; }

        public string AboutMe { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string StackoverflowUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string LinkedinUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string InstagramUrl { get; set; }

        public bool IsBlocked { get; set; }

        public string ReasonToBeBlocked { get; set; }

        public ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();
    }
}