using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SmartHomeManager.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SMTPSettings _smtpSettings;

        public EmailService(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string subject, string body, string receiverEmail, string filePath = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Smart Home Manager", _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", receiverEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = body
                };

                if (filePath != null)
                {
                    bodyBuilder.Attachments.Add(filePath);
                }

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, true);
                    await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
                return false;
            }
        }

    }
}