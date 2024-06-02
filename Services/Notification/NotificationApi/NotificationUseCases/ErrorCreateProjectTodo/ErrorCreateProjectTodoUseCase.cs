namespace NotificationApi.NotificationUseCases.ErrorCreateProjectTodo;

public record ErrorCreateProjectTodoCommand(string message);
public record ErrorUpdateProjectResult(bool isSuccess);


public class ErrorCreateProjectTodoUseCase(
    ISmtpService smtpService, 
    IEmailUserRepository emailUserRepository, 
    INotificationEmailRepository notificationEmailRepository, 
    IConfiguration configuration) : IErrorCreateProjectTodoUseCase
{
    public async Task<ErrorUpdateProjectResult> Execute(ErrorCreateProjectTodoCommand command)
    {
        MailAddress fromAddress = new MailAddress(configuration["FromMailAddress"]);
        var notificationEmail = await notificationEmailRepository.GetNotificationEmails();

        MailModel mailModel = new()
        {
            ToAddress = notificationEmail.Select(x => x.Email).ToList(),
            FromAddress = fromAddress,
            Subject = "Errors when creating a project task",
            Body = command.message,
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorUpdateProjectResult(result);
    }
}
