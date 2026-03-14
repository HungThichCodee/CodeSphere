using System.ComponentModel.DataAnnotations;

namespace CodeSphere.ApplicationAttributes.ValidationAttributes
{
    public class FilesCollectionRange : ValidationAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;

        public FilesCollectionRange(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = (HashSet<IFormFile>)value;

            if (collection.Count >= this.minLength && collection.Count <= this.maxLength)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                $"Collection items count should be in range [{this.minLength} - {this.maxLength}].");
        }
    }
}
