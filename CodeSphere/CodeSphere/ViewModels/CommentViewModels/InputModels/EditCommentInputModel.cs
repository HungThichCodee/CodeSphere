using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace CodeSphere.ViewModels.CommentViewModels.InputModels
{
    public class EditCommentInputModel
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        //[Required]
        public string? PostId { get; set; }

        public string? ParentCommentId { get; set; }
    }
}
