using CleanFirmeza.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CleanFirmeza.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse("tucorreo@gmail.com"));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync("tucorreo@gmail.com", "CLAVE_DE_APLICACION");
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}