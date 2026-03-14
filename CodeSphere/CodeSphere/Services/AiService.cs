using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CodeSphere.SecurityModels;
using CodeSphere.ViewModels.Blog.ViewModels;
using Microsoft.Extensions.Options;

namespace CodeSphere.Services
{
    public class AiService : IAiService
    {
        private readonly GroqSettings groqSettings;
        private readonly HttpClient httpClient;
        private readonly ILogger<AiService> logger;

        public AiService(IOptions<GroqSettings> groqSettings, IHttpClientFactory httpClientFactory, ILogger<AiService> logger)
        {
            this.groqSettings = groqSettings.Value;
            this.logger = logger;
            this.httpClient = httpClientFactory.CreateClient();
            // Đảm bảo BaseUrl trong appsettings đã có dấu / ở cuối (https://api.groq.com/openai/v1/)
            this.httpClient.BaseAddress = new Uri(this.groqSettings.BaseUrl);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.groqSettings.ApiKey);
            // Header version bắt buộc cho Groq
            this.httpClient.DefaultRequestHeaders.Add("groq-version", "2024-06-18");
        }

        public async Task<string> GeneratePostContentAsync(string topic, string? additionalContext = null)
        {
            try
            {
                this.logger.LogInformation("Starting to generate post content for topic: {Topic}", topic);

                var prompt = $"Viết một bài viết blog chuyên nghiệp về chủ đề: {topic}";
                if (!string.IsNullOrEmpty(additionalContext))
                {
                    prompt += $"\n\nYêu cầu bổ sung: {additionalContext}";
                }

                prompt += "\n\nHãy viết bài viết với cấu trúc rõ ràng, có tiêu đề và nội dung chi tiết, phù hợp với blog công nghệ. Định dạng với markdown.";

                var requestBody = new
                {
                    model = this.groqSettings.Model, // Lấy từ appsettings (llama-3.1-8b-instant)
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = prompt
                        }
                    },
                    temperature = 0.7,
                    max_tokens = 2000
                };

                var jsonContent = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // SỬA ĐỔI: Bỏ dấu / ở đầu string format log để hiển thị đúng
                this.logger.LogInformation("Sending request to Groq API: {Url}", this.httpClient.BaseAddress + "chat/completions");

                // SỬA ĐỔI QUAN TRỌNG: Bỏ dấu / ở đầu endpoint
                // Vì BaseAddress đã kết thúc bằng /, nên ở đây chỉ cần "chat/completions"
                // Nếu để "/chat/completions", HttpClient sẽ ghi đè BasePath và gây lỗi 404.
                var response = await this.httpClient.PostAsync("chat/completions", httpContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogError("Groq API returned error. Status: {Status}, Response: {Response}",
                        response.StatusCode, responseContent);
                    throw new HttpRequestException($"Groq API error: {response.StatusCode} - {responseContent}");
                }

                this.logger.LogInformation("Groq API response received successfully");

                var jsonDoc = JsonDocument.Parse(responseContent);

                if (!jsonDoc.RootElement.TryGetProperty("choices", out var choices) ||
                    choices.GetArrayLength() == 0)
                {
                    this.logger.LogError("Invalid response format from Groq API: {Response}", responseContent);
                    throw new InvalidOperationException("Invalid response format from Groq API");
                }

                var generatedContent = choices[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (string.IsNullOrEmpty(generatedContent))
                {
                    this.logger.LogWarning("Generated content is empty");
                    return "Không thể tạo nội dung. Vui lòng thử lại.";
                }

                this.logger.LogInformation("Successfully generated content with length: {Length}", generatedContent.Length);
                return generatedContent;
            }
            catch (HttpRequestException ex)
            {
                this.logger.LogError(ex, "HTTP error when calling Groq API");
                throw new Exception("Lỗi kết nối đến Groq API. Vui lòng kiểm tra API key và kết nối mạng.", ex);
            }
            catch (JsonException ex)
            {
                this.logger.LogError(ex, "Error parsing JSON response from Groq API");
                throw new Exception("Lỗi xử lý phản hồi từ Groq API.", ex);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unexpected error generating post content with Groq API");
                throw new Exception("Có lỗi xảy ra khi tạo bài viết. Vui lòng thử lại sau.", ex);
            }
        }

        public async Task<AiGeneratedPostViewModel> GeneratePostAsync(string topic, string? additionalContext = null)
        {
            var content = await this.GeneratePostContentAsync(topic, additionalContext);

            return new AiGeneratedPostViewModel
            {
                Content = content,
                Topic = topic
            };
        }
    }
}