using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace NotMvcButCoreWebUi.Services
{
    public class EmailSender :IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Sender(System.String Message)
        {
            var apiKey = _configuration.GetSection("Apis")["SendGridApi"];// Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cahatayozdemir@gmail.com","Test Maili");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("test@example.com","Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from,to,subject,plainTextContent,htmlContent);
            // var response 
            await client.SendEmailAsync(msg);
        }
    }
}
