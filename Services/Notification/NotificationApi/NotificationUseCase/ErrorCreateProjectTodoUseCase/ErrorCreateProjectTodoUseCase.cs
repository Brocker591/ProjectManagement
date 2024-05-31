namespace NotificationApi.NotificationUseCase.ErrorCreateProjectTodoUseCase;

public record ErrorCreateProjectTodoCommand(string message);
public record ErrorUpdateProjectResult(bool isSuccess);


public class ErrorCreateProjectTodoUseCase(ISmtpService smtpService, IEmailUserRepository repository) : IErrorCreateProjectTodoUseCase
{
    public async Task<ErrorUpdateProjectResult> Execute(ErrorCreateProjectTodoCommand command)
    {
        //TODO User muss erstellt werden

        MailModel mailModel = new()
        {
            ToAddress = new MailAddress("ToAddress"),
            FromAddress = new MailAddress("FromAddress"),
            Subject = "Errors when creating a project task",
            Body = command.message,
            EmailUsers = await repository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorUpdateProjectResult(result);
    }
}
