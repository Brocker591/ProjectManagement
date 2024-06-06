namespace NotificationApi.NotificationUseCases.ErrorDeleteProject;

public record ErrorDeleteProjectCommand(string message, DeleteProjectEvent eventObject);
public record ErrorDeleteProjectResult(bool isSuccess);

public class ErrorDeleteProjectUseCase(
    ISmtpService smtpService,
    IEmailUserRepository emailUserRepository,
    INotificationEmailRepository notificationEmailRepository,
    IConfiguration configuration) : IErrorDeleteProjectUseCase
{
    public async Task<ErrorDeleteProjectResult> Execute(ErrorDeleteProjectCommand command)
    {
        MailAddress fromAddress = new MailAddress(configuration["FromMailAddress"]);
        var notificationEmail = await notificationEmailRepository.GetNotificationEmails();

        MailModel mailModel = new()
        {
            ToAddress = notificationEmail.Select(x => new MailAddress(x.Email)).ToList(),
            FromAddress = fromAddress,
            Subject = "An error occurred when deleting the project",
            Body = $"Message: {command.message}\nEvent: {JsonSerializer.Serialize(command.eventObject)}",
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorDeleteProjectResult(result);
    }
}
