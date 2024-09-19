using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OA.Domain.Settings;
using OA.Service.Contract;
using OA.Service.Exceptions;

namespace OA.Service.Implementation;

public class MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
    : IEmailService
{
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.From ?? mailSettings.Value.EmailFrom);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(mailSettings.Value.SmtpHost, mailSettings.Value.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailSettings.Value.SmtpUser, mailSettings.Value.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

        }
        catch (System.Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new ApiException(ex.Message);
        }
    }

}