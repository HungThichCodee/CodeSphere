namespace CodeSphere.SecurityModels
{
    public class GroqSettings
    {
        public string ApiKey { get; set; } = string.Empty;

        public string BaseUrl { get; set; } = "https://api.groq.com/openai/v1/";

        public string Model { get; set; } = "llama-3.1-8b-instant"; 
    }
}