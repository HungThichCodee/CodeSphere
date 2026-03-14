using System.ComponentModel.DataAnnotations;
using CodeSphere.Constraints;

namespace CodeSphere.Models.User
{
    public class UserRating
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? RaterUsername { get; set; }

        [Required]
        [MaxLength(ModelConstraints.RatingStarsMaxValue)]
        public int Stars { get; set; }
    }
}
