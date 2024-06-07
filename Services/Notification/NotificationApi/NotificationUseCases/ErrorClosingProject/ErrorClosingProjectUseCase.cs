using Common.MessageEvents;
using NotificationApi.NotificationUseCases.ErrorDeleteProject;

namespace NotificationApi.NotificationUseCases.ErrorClosingProject;

public record ErrorClosingProjectCommand(string message, ClosingProjectEvent eventObject);
public record ErrorClosingProjectResult(bool isSuccess);

public class ErrorClosingProjectUseCase(
    ISmtpService smtpService,
    IEmailUserRepository emailUserRepository,
    INotificationEmailRepository notificationEmailRepository,
    IConfiguration configuration) : IErrorClosingProjectUseCase
{
    public async Task<ErrorClosingProjectResult> Execute(ErrorClosingProjectCommand command)
    {
        MailAddress fromAddress = new MailAddress(configuration["FromMailAddress"]);
        var notificationEmail = await notificationEmailRepository.GetNotificationEmails();

        MailModel mailModel = new()
        {
            ToAddress = notificationEmail.Select(x => new MailAddress(x.Email)).ToList(),
            FromAddress = fromAddress,
            Subject = "An error occurred when closing the project",
            Body = $"Message: {command.message}\nEvent: {JsonSerializer.Serialize(command.eventObject)}",
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorClosingProjectResult(result);
    }

}
