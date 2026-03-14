using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace CodeSphere.ViewModels.CommentViewModels.InputModels
{
    public class CreateCommentInputModel
    {
        public string? PostId { get; set; }

        public string? ParentId { get; set; }

        [Required]
        public string? Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(Content);
    }
}
