using System.Net.Mail;
using System.Net;
using CodeSphere.ViewModels.Contacts;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace CodeSphere.Repositories.ContactRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IConfiguration configuration;

        public ContactRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(ContactInputModel model)
        {
            Execute(model).Wait();
        }

        private async Task Execute(ContactInputModel model)
        {
            var apiKey = configuration.GetSection("SendGrid:ApiKey").Value;
            var client = new SendGridClient(apiKey);

            var message = new SendGridMessage()
            {
                From = new EmailAddress(model.Email, model.Name),
                Subject = model.Subject,
                PlainTextContent = model.Message,
                HtmlContent = $"<strong>Hello, CodeSphere Administrators!</strong><br />{model.Message}",
            };

            message.AddTo(new EmailAddress("dungtrantrung603@gmail.com", "CodeSphere"));
            var response = await client.SendEmailAsync(message);
        }
    }
}
