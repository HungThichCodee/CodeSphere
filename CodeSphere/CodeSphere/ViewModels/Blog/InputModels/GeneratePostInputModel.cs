using System.ComponentModel.DataAnnotations;

namespace CodeSphere.ViewModels.Blog.InputModels
{
    public class GeneratePostInputModel
    {
        [Required(ErrorMessage = "Vui lòng nhập chủ đề bài viết")]
        [Display(Name = "Chủ đề bài viết")]
        [MaxLength(200, ErrorMessage = "Chủ đề không được vượt quá 200 ký tự")]
        public string Topic { get; set; } = string.Empty;

        [Display(Name = "Yêu cầu bổ sung (tùy chọn)")]
        [MaxLength(500, ErrorMessage = "Yêu cầu bổ sung không được vượt quá 500 ký tự")]
        public string? AdditionalContext { get; set; }
    }
}