using Common.MessageEvents;
using System.Text.Json;

namespace NotificationApi.NotificationUseCases.ErrorCreateProjectTodo;

public record ErrorCreateProjectTodoCommand(string message, CreateProjectTodoEvent eventObject);
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
            ToAddress = notificationEmail.Select(x => new MailAddress(x.Email)).ToList(),
            FromAddress = fromAddress,
            Subject = "Errors when creating a project task",
            Body = $"Message: {command.message}\nEvent: {JsonSerializer.Serialize(command.eventObject)}",
            EmailUsers = await emailUserRepository.GetEmailUsers()
        };

        bool result = await smtpService.SendEmail(mailModel);

        return new ErrorUpdateProjectResult(result);
    }
}
