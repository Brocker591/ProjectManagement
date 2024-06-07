namespace NotificationApi.NotificationUseCases.SendNotification;

public record SendNotificationCommand(string Subject, string Body);
public record SendNotificationResult(bool isSending);

public class SendNotificationUseCase(
    ISmtpService smtpService,
    IEmailUserRepository emailUserRepository,
    INotificationEmailRepository notificationEmailRepository,
    IConfiguration configuration) : ISendNotificationUseCase
{

    public async Task<SendNotificationResult> Execute(SendNotificationCommand command)
    {
        MailAddress fromAddress = new MailAddress(configuration["FromMailAddress"]);
        var notificationEmail = await notificationEmailRepository.GetNotificationEmails();

        MailModel mailModel = new()
        {
            ToAddress = notificationEmail.Select(x => new MailAddress(x.Email)).ToList(),
            FromAddress = fromAddress,
            Subject = command.Subject,
            Body = command.Body,
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new SendNotificationResult(result);
    }
}
