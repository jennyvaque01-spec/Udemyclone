namespace UdemyClone.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string toEmail, string nombre);
        Task SendLoginNotificationAsync(string toEmail);
        Task SendPasswordResetAsync(string toEmail, string resetLink);
    }
}


