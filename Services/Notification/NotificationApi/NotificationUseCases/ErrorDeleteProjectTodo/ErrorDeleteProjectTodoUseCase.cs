namespace NotificationApi.NotificationUseCases.ErrorDeleteProjectTodo;

public record ErrorDeleteProjectTodoCommand(string message);
public record ErrorUpdateProjectResult(bool isSuccess);

public class ErrorDeleteProjectTodoUseCase(
    ISmtpService smtpService,
    IEmailUserRepository emailUserRepository,
    INotificationEmailRepository notificationEmailRepository,
    IConfiguration configuration) : IErrorDeleteProjectTodoUseCase
{
    public async Task<ErrorUpdateProjectResult> Execute(ErrorDeleteProjectTodoCommand command)
    {
        MailAddress fromAddress = new MailAddress(configuration["FromMailAddress"]);
        var notificationEmail = await notificationEmailRepository.GetNotificationEmails();

        MailModel mailModel = new()
        {
            ToAddress = notificationEmail.Select(x => x.Email).ToList(),
            FromAddress = fromAddress,
            Subject = "Errors when delete a project task",
            Body = command.message,
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorUpdateProjectResult(result);
    }
}
