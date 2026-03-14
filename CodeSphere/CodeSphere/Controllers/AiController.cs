using System.Text;
using System.Text.Json;
using CodeSphere.Constraints;
using CodeSphere.Models.User;
using CodeSphere.Repositories.AiRepositories;
using CodeSphere.Repositories.BlogRepositories;
using CodeSphere.Services;
using CodeSphere.ViewModels.Blog.InputModels;
using CodeSphere.ViewModels.Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Controllers
{
    [Authorize]
    public class AiController : Controller
    {
        private readonly IAiService aiService;
        private readonly IAiRepository aiRepository;
        private readonly IBlogRepository blogRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AiController> logger;

        public AiController(
            IAiService aiService,
            IAiRepository aiRepository,
            IBlogRepository blogRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<AiController> logger)
        {
            this.aiService = aiService;
            this.aiRepository = aiRepository;
            this.blogRepository = blogRepository;
            this.userManager = userManager;
            this.logger = logger;
        }

        // ... (Giữ nguyên các action GeneratePost và PreviewPost) ...

        [HttpGet]
        public IActionResult GeneratePost()
        {
            var model = new GeneratePostInputModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePost(GeneratePostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (user == null || user.IsBlocked == true)
            {
                this.TempData["Error"] = ErrorMessages.YouAreBlock;
                return this.RedirectToAction("Index", "Blog");
            }

            try
            {
                this.logger.LogInformation("User {UserId} is generating post with topic: {Topic}", user.Id, model.Topic);

                var generatedPost = await this.aiService.GeneratePostAsync(
                    model.Topic,
                    model.AdditionalContext);

                var title = this.ExtractTitleFromContent(generatedPost.Content, model.Topic);
                generatedPost.ExtractedTitle = title;

                var similarPosts = await this.aiRepository.FindSimilarPostsAsync(
                    topic: model.Topic,
                    content: generatedPost.Content,
                    categoryName: null,
                    tags: null,
                    count: 5);

                generatedPost.SimilarPosts = similarPosts;

                this.HttpContext.Session.SetString("AiGeneratedContent", generatedPost.Content);
                this.HttpContext.Session.SetString("AiGeneratedTitle", title);
                this.HttpContext.Session.SetString("AiGeneratedTopic", model.Topic);

                var previewModel = new AiPostPreviewViewModel
                {
                    GenerateInput = model,
                    GeneratedPost = generatedPost,
                    Categories = await this.blogRepository.ExtractAllCategoryNames(),
                    Tags = await this.blogRepository.ExtractAllTagNames()
                };

                this.logger.LogInformation("Successfully generated post for user {UserId}", user.Id);
                return this.View("PreviewPost", previewModel);
            }
            catch (HttpRequestException ex)
            {
                this.logger.LogError(ex, "HTTP error generating post with AI for user {UserId}", user?.Id);
                this.TempData["Error"] = $"Lỗi kết nối: {ex.Message}. Vui lòng kiểm tra API key và thử lại.";
                return this.View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error generating post with AI for user {UserId}. Error: {Error}",
                    user?.Id, ex.Message);
                this.TempData["Error"] = $"Có lỗi xảy ra: {ex.Message}. Vui lòng thử lại.";
                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> PreviewPostAsync()
        {
            var content = this.HttpContext.Session.GetString("AiGeneratedContent");
            var title = this.HttpContext.Session.GetString("AiGeneratedTitle");
            var topic = this.HttpContext.Session.GetString("AiGeneratedTopic");

            if (string.IsNullOrEmpty(content))
            {
                this.TempData["Error"] = "Không tìm thấy bài viết đã tạo. Vui lòng tạo lại.";
                return this.RedirectToAction("GeneratePost");
            }

            var model = new AiPostPreviewViewModel
            {
                GeneratedPost = new AiGeneratedPostViewModel
                {
                    Content = content,
                    ExtractedTitle = title ?? string.Empty,
                    Topic = topic ?? string.Empty
                },
                Categories = await this.blogRepository.ExtractAllCategoryNames(),
                Tags = await this.blogRepository.ExtractAllTagNames()
            };

            return this.View(model);
        }

        // --- SỬA ĐỔI QUAN TRỌNG Ở ĐÂY ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(CreatePostInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (user == null || user.IsBlocked == true)
            {
                this.TempData["Error"] = ErrorMessages.YouAreBlock;
                return this.RedirectToAction("Index", "Blog");
            }

            // 1. Lấy content từ Session
            var content = this.HttpContext.Session.GetString("AiGeneratedContent");

            if (string.IsNullOrEmpty(content))
            {
                this.TempData["Error"] = "Không tìm thấy bài viết đã tạo. Vui lòng tạo lại.";
                return this.RedirectToAction("GeneratePost");
            }

            // 2. Convert Markdown sang HTML để TinyMCE hiển thị đẹp
            var htmlContent = Markdig.Markdown.ToHtml(content);

            // 3. Lưu tất cả dữ liệu vào Session để pre-fill trong CreatePost
            this.HttpContext.Session.SetString("AiPostTitle", model.Title ?? this.HttpContext.Session.GetString("AiGeneratedTitle") ?? string.Empty);
            this.HttpContext.Session.SetString("AiPostContent", htmlContent); // Lưu HTML thay vì Markdown
            this.HttpContext.Session.SetString("AiPostCategory", model.CategoryName ?? string.Empty);

            // Lưu TagsNames dưới dạng JSON string
            if (model.TagsNames != null && model.TagsNames.Any())
            {
                var tagsJson = System.Text.Json.JsonSerializer.Serialize(model.TagsNames);
                this.HttpContext.Session.SetString("AiPostTags", tagsJson);
            }
            else
            {
                this.HttpContext.Session.Remove("AiPostTags");
            }

            // 4. Lưu flag để biết đây là post từ AI
            this.HttpContext.Session.SetString("IsFromAI", "true");

            // 5. Redirect đến CreatePost để chỉnh sửa với TinyMCE
            return this.RedirectToAction("CreatePost", "Blog");
        }

        private string ExtractTitleFromContent(string content, string topic)
        {
            var lines = content.Split('\n');
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("# "))
                {
                    return trimmed.Substring(2).Trim();
                }
            }
            return topic;
        }
    }
}
