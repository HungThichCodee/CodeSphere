using CodeSphere.Data;
using CodeSphere.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeSphere.Repositories.AiRepositories
{
    public class AiRepository : IAiRepository
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<AiRepository> logger;

        public AiRepository(ApplicationDbContext db, ILogger<AiRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<ICollection<Post>> FindSimilarPostsAsync(string topic, string? content, string? categoryName, ICollection<string>? tags, int count = 5, string? excludePostId = null)
        {
            try
            {
                this.logger.LogInformation("Finding similar posts for topic: {Topic}", topic);

                // Lấy tất cả posts (trừ post hiện tại nếu có)
                var allPosts = await this.db.Posts
                    .Where(p => p.Id != excludePostId)
                    .Include(p => p.Category)
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.PostsTags)
                        .ThenInclude(pt => pt.Tag)
                    .ToListAsync();

                // Tính điểm similarity cho mỗi post
                var postScores = new List<(Post Post, float Score)>();

                // Extract keywords từ topic và content
                var topicKeywords = this.ExtractKeywords(topic);
                var contentKeywords = content != null ? this.ExtractKeywords(content) : new HashSet<string>();

                foreach (var post in allPosts)
                {
                    float score = 0f;

                    // 1. So sánh Category (40% điểm)
                    if (!string.IsNullOrEmpty(categoryName) && 
                        post.Category != null && 
                        post.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 0.4f;
                    }

                    // 2. So sánh Tags (30% điểm)
                    if (tags != null && tags.Any() && post.PostsTags.Any())
                    {
                        var postTagNames = post.PostsTags
                            .Select(pt => pt.Tag?.Name?.ToLower())
                            .Where(name => !string.IsNullOrEmpty(name))
                            .ToHashSet();

                        var matchingTags = tags
                            .Select(t => t.ToLower())
                            .Count(t => postTagNames.Contains(t));

                        if (matchingTags > 0)
                        {
                            score += 0.3f * (matchingTags / (float)Math.Max(tags.Count, postTagNames.Count));
                        }
                    }

                    // 3. So sánh Keywords trong Title (20% điểm)
                    if (!string.IsNullOrEmpty(post.Title))
                    {
                        var postTitleKeywords = this.ExtractKeywords(post.Title);
                        var titleMatches = topicKeywords.Count(k => postTitleKeywords.Contains(k));
                        if (titleMatches > 0)
                        {
                            score += 0.2f * (titleMatches / (float)Math.Max(topicKeywords.Count, postTitleKeywords.Count));
                        }
                    }

                    // 4. So sánh Keywords trong Content (10% điểm)
                    if (!string.IsNullOrEmpty(post.Content) && contentKeywords.Any())
                    {
                        var postContentKeywords = this.ExtractKeywords(post.Content);
                        var contentMatches = contentKeywords.Count(k => postContentKeywords.Contains(k));
                        if (contentMatches > 0)
                        {
                            score += 0.1f * (contentMatches / (float)Math.Max(contentKeywords.Count, postContentKeywords.Count));
                        }
                    }

                    if (score > 0)
                    {
                        postScores.Add((post, score));
                    }
                }

                // Sắp xếp theo score và lấy top posts
                var similarPosts = postScores
                    .OrderByDescending(x => x.Score)
                    .ThenByDescending(x => x.Post.CreatedOn) // Nếu score bằng nhau, ưu tiên bài mới
                    .Take(count)
                    .Select(x => x.Post)
                    .ToList();

                // Nếu không tìm thấy bài tương tự, trả về các bài mới nhất
                if (!similarPosts.Any())
                {
                    this.logger.LogInformation("No similar posts found, returning latest posts");
                    similarPosts = allPosts
                        .OrderByDescending(p => p.CreatedOn)
                        .Take(count)
                        .ToList();
                }

                this.logger.LogInformation("Found {Count} similar posts", similarPosts.Count);
                return similarPosts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error finding similar posts");
                // Trả về các bài mới nhất nếu có lỗi
                try
                {
                    return await this.db.Posts
                        .Where(p => p.Id != excludePostId)
                        .Include(p => p.Category)
                        .Include(p => p.ApplicationUser)
                        .OrderByDescending(p => p.CreatedOn)
                        .Take(count)
                        .ToListAsync();
                }
                catch
                {
                    return new List<Post>();
                }
            }
        }

        private HashSet<string> ExtractKeywords(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new HashSet<string>();
            }

            // Loại bỏ các ký tự đặc biệt và chuyển thành lowercase
            var words = text
                .ToLower()
                .Split(new[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}' }, 
                    StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > 2) // Bỏ qua từ quá ngắn
                .Where(w => !this.IsStopWord(w)) // Bỏ qua stop words
                .ToHashSet();

            return words;
        }

        private bool IsStopWord(string word)
        {
            // Danh sách stop words tiếng Việt và tiếng Anh
            var stopWords = new HashSet<string>
            {
                "the", "a", "an", "and", "or", "but", "in", "on", "at", "to", "for", "of", "with", "by",
                "và", "của", "trong", "với", "cho", "từ", "về", "đến", "được", "là", "có", "một", "các",
                "này", "đó", "nào", "khi", "nếu", "vì", "sau", "trước", "như", "để", "mà", "thì", "cũng"
            };

            return stopWords.Contains(word.ToLower());
        }
    }
}