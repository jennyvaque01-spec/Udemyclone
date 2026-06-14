using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models;

namespace UdemyClone.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string nombre)
        {
            var subject = "¡Bienvenido a Tech Academy!";
            var body = BuildWelcomeTemplate(nombre);
            await SendAsync(toEmail, nombre, subject, body);
        }

        public async Task SendLoginNotificationAsync(string toEmail)
        {
            var subject = "Nuevo inicio de sesión detectado";
            var body = BuildLoginTemplate(toEmail);
            await SendAsync(toEmail, toEmail, subject, body);
        }

        public async Task SendPasswordResetAsync(string toEmail, string resetLink)
        {
            var subject = "Restablece tu contraseña";
            var body = BuildResetTemplate(resetLink);
            await SendAsync(toEmail, toEmail, subject, body);
        }

        private async Task SendAsync(string toEmail, string toName, string subject, string htmlBody)
        {
            try
            {
                if (_settings.Provider?.ToLower() == "mailjet")
                    await SendWithMailjet(toEmail, toName, subject, htmlBody);
                else
                    await SendWithSmtp(toEmail, toName, subject, htmlBody);

                _logger.LogInformation("Correo enviado a {Email} | Asunto: {Subject}", toEmail, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo a {Email}", toEmail);
                throw;
            }
        }

        private async Task SendWithSmtp(string toEmail, string toName, string subject, string htmlBody)
        {
            using var client = new SmtpClient(_settings.Smtp.Host, _settings.Smtp.Port)
            {
                EnableSsl = _settings.Smtp.UseSsl,
                Credentials = !string.IsNullOrEmpty(_settings.Smtp.User)
                    ? new NetworkCredential(_settings.Smtp.User, _settings.Smtp.Password)
                    : null
            };

            var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(toEmail, toName));
            await client.SendMailAsync(message);
        }

        private async Task SendWithMailjet(string toEmail, string toName, string subject, string htmlBody)
        {
            var client = new MailjetClient(_settings.Mailjet.ApiKey, _settings.Mailjet.SecretKey);
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_settings.FromEmail, _settings.FromName))
                .WithSubject(subject)
                .WithHtmlPart(htmlBody)
                .WithTo(new SendContact(toEmail, toName))
                .Build();

            var response = await client.SendTransactionalEmailAsync(email);
            if (response.Messages.Any(m => m.Status != "success"))
                throw new Exception("Mailjet: error al enviar el correo");
        }

        private static string BuildWelcomeTemplate(string nombre) => $@"
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset='UTF-8'>
          <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        </head>
        <body style='margin:0;padding:0;background:#0F172A;font-family:Arial,sans-serif;'>
          <table width='100%' cellpadding='0' cellspacing='0'
                 style='background:#0F172A;padding:40px 20px;'>
            <tr>
              <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0'
                       style='background:#1E293B;border-radius:16px;
                              border:1px solid #2E3A4E;overflow:hidden;
                              max-width:600px;width:100%;'>

                  <tr>
                    <td style='background:linear-gradient(135deg,#EC4899,#7C3AED);
                               padding:40px 40px 30px;text-align:center;'>
                      <h1 style='color:white;margin:0;font-size:28px;font-weight:800;'>
                        Tech Academy
                      </h1>
                      <p style='color:rgba(255,255,255,0.85);margin:8px 0 0;font-size:14px;'>
                        Plataforma de Aprendizaje en Línea
                      </p>
                    </td>
                  </tr>

                  <tr>
                    <td style='padding:40px;'>
                      <h2 style='color:#F8FAFC;font-size:22px;margin:0 0 16px;'>
                        ¡Bienvenido, {nombre}! 
                      </h2>
                      <p style='color:#94A3B8;font-size:15px;line-height:1.7;margin:0 0 24px;'>
                        Nos alegra tenerte en <strong style='color:#A855F7;'>Tech Academy</strong>.
                        Tu cuenta ha sido creada exitosamente y ya puedes acceder
                        a todos nuestros cursos.
                      </p>

                      <table width='100%' cellpadding='0' cellspacing='0'
                             style='margin:0 0 28px;'>
                        <tr>
                          <td style='padding:4px;width:33%;'>
                            <div style='background:#162032;border:1px solid #2E3A4E;
                                        border-radius:10px;padding:16px;text-align:center;'>
                              <div style='font-size:24px;margin-bottom:6px;'></div>
                              <div style='color:#F8FAFC;font-size:12px;font-weight:600;'>
                                Cursos disponibles
                              </div>
                            </div>
                          </td>
                          <td style='padding:4px;width:33%;'>
                            <div style='background:#162032;border:1px solid #2E3A4E;
                                        border-radius:10px;padding:16px;text-align:center;'>
                              <div style='font-size:24px;margin-bottom:6px;'></div>
                              <div style='color:#F8FAFC;font-size:12px;font-weight:600;'>
                                Certificados
                              </div>
                            </div>
                          </td>
                          <td style='padding:4px;width:33%;'>
                            <div style='background:#162032;border:1px solid #2E3A4E;
                                        border-radius:10px;padding:16px;text-align:center;'>
                              <div style='font-size:24px;margin-bottom:6px;'></div>
                              <div style='color:#F8FAFC;font-size:12px;font-weight:600;'>
                                Acceso seguro
                              </div>
                            </div>
                          </td>
                        </tr>
                      </table>

                      
                      <div style='text-align:center;margin:0 0 28px;'>
                        <a href='http://localhost:4200/login'
                           style='display:inline-block;
                                  background:linear-gradient(135deg,#EC4899,#7C3AED);
                                  color:white;padding:14px 36px;border-radius:10px;
                                  text-decoration:none;font-weight:700;font-size:15px;'>
                          Ingresar a la plataforma
                        </a>
                      </div>

                      <p style='color:#64748B;font-size:13px;margin:0;
                                border-top:1px solid #2E3A4E;padding-top:20px;'>
                        Si no creaste esta cuenta, puedes ignorar este mensaje.
                        Para soporte escríbenos a
                        <a href='mailto:info@techacademy.com'
                           style='color:#A855F7;'>info@techacademy.com</a>
                      </p>
                    </td>
                  </tr>

                  <!-- Footer -->
                  <tr>
                    <td style='background:#162032;padding:20px 40px;text-align:center;
                               border-top:1px solid #2E3A4E;'>
                      <p style='color:#475569;font-size:12px;margin:0;'>
                        © 2026 Tech Academy · Desarrollado por Jenny Vaque
                      </p>
                    </td>
                  </tr>

                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";

        private static string BuildLoginTemplate(string email) => $@"
        <!DOCTYPE html>
        <html>
        <head><meta charset='UTF-8'></head>
        <body style='margin:0;padding:0;background:#0F172A;font-family:Arial,sans-serif;'>
          <table width='100%' cellpadding='0' cellspacing='0'
                 style='background:#0F172A;padding:40px 20px;'>
            <tr>
              <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0'
                       style='background:#1E293B;border-radius:16px;
                              border:1px solid #2E3A4E;overflow:hidden;
                              max-width:600px;width:100%;'>

                  <tr>
                    <td style='background:linear-gradient(135deg,#1E3A8A,#3B82F6);
                               padding:32px 40px;text-align:center;'>
                      <div style='font-size:48px;margin-bottom:10px;'></div>
                      <h2 style='color:white;margin:0;font-size:22px;font-weight:700;'>
                        Nuevo inicio de sesión
                      </h2>
                    </td>
                  </tr>

                  <tr>
                    <td style='padding:36px 40px;'>
                      <p style='color:#94A3B8;font-size:15px;line-height:1.7;margin:0 0 20px;'>
                        Hemos detectado un nuevo inicio de sesión en tu cuenta
                        <strong style='color:#F8FAFC;'>{email}</strong>
                        en Tech Academy.
                      </p>

                      <div style='background:#162032;border:1px solid #2E3A4E;
                                  border-radius:12px;padding:20px;margin:0 0 24px;'>
                        <p style='color:#94A3B8;font-size:13px;margin:0 0 8px;'>
                          Fecha y hora
                        </p>
                        <p style='color:#F8FAFC;font-size:14px;font-weight:600;margin:0;'>
                          {DateTime.UtcNow:dd/MM/yyyy HH:mm} UTC
                        </p>
                      </div>

                      <p style='color:#64748B;font-size:13px;margin:0;
                                border-top:1px solid #2E3A4E;padding-top:20px;'>
                        Si no fuiste tú, cambia tu contraseña inmediatamente o
                        contáctanos en
                        <a href='mailto:info@techacademy.com'
                           style='color:#A855F7;'>info@techacademy.com</a>
                      </p>
                    </td>
                  </tr>

                  <tr>
                    <td style='background:#162032;padding:20px 40px;text-align:center;
                               border-top:1px solid #2E3A4E;'>
                      <p style='color:#475569;font-size:12px;margin:0;'>
                        © 2026 Tech Academy · Desarrollado por Jenny Vaque
                      </p>
                    </td>
                  </tr>

                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";

        private static string BuildResetTemplate(string resetLink) => $@"
        <!DOCTYPE html>
        <html>
        <head><meta charset='UTF-8'></head>
        <body style='margin:0;padding:0;background:#0F172A;font-family:Arial,sans-serif;'>
          <table width='100%' cellpadding='0' cellspacing='0'
                 style='background:#0F172A;padding:40px 20px;'>
            <tr>
              <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0'
                       style='background:#1E293B;border-radius:16px;
                              border:1px solid #2E3A4E;overflow:hidden;
                              max-width:600px;width:100%;'>

                  <tr>
                    <td style='background:linear-gradient(135deg,#7C3AED,#EC4899);
                               padding:32px 40px;text-align:center;'>
                      <div style='font-size:48px;margin-bottom:10px;'>🔑</div>
                      <h2 style='color:white;margin:0;font-size:22px;font-weight:700;'>
                        Restablecer contraseña
                      </h2>
                    </td>
                  </tr>

                  <tr>
                    <td style='padding:36px 40px;'>
                      <p style='color:#94A3B8;font-size:15px;line-height:1.7;margin:0 0 28px;'>
                        Recibimos una solicitud para restablecer la contraseña
                        de tu cuenta en Tech Academy. Haz clic en el botón
                        para continuar. Este enlace expira en <strong style='color:#F8FAFC;'>
                        30 minutos</strong>.
                      </p>

                      <div style='text-align:center;margin:0 0 28px;'>
                        <a href='{resetLink}'
                           style='display:inline-block;
                                  background:linear-gradient(135deg,#7C3AED,#EC4899);
                                  color:white;padding:14px 36px;border-radius:10px;
                                  text-decoration:none;font-weight:700;font-size:15px;'>
                           Restablecer contraseña
                        </a>
                      </div>

                      <p style='color:#64748B;font-size:13px;margin:0;
                                border-top:1px solid #2E3A4E;padding-top:20px;'>
                        Si no solicitaste esto, ignora este mensaje.
                        Tu contraseña permanecerá sin cambios.
                      </p>
                    </td>
                  </tr>

                  <tr>
                    <td style='background:#162032;padding:20px 40px;text-align:center;
                               border-top:1px solid #2E3A4E;'>
                      <p style='color:#475569;font-size:12px;margin:0;'>
                        © 2026 Tech Academy · Desarrollado por Jenny Vaque
                      </p>
                    </td>
                  </tr>

                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";
    }
}

