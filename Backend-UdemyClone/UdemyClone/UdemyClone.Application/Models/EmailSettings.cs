namespace UdemyClone.Application.Models
{
    public class EmailSettings
    {
        public string Provider { get; set; } = "smtp";
        public SmtpSettings Smtp { get; set; } = new();
        public MailjetSettings Mailjet { get; set; } = new();
        public string FromEmail { get; set; } = null!;
        public string FromName { get; set; } = null!;
    }

    public class SmtpSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 25;
        public bool UseSsl { get; set; } = false;
        public string? User { get; set; }
        public string? Password { get; set; }
    }

    public class MailjetSettings
    {
        public string ApiKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
