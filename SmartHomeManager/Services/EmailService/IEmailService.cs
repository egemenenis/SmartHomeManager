using System.Threading.Tasks;

namespace SmartHomeManager.Services.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string subject, string body, string receiverEmail, string filePath = null);
    }
}