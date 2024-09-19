using OA.Domain.Settings;

namespace OA.Service.Contract;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);

}