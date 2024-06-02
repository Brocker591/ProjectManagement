namespace NotificationApi.NotificationEmailUseCases.CreateNotificationEmail;

public record CreateNotificationEmailCommand(MailAddress Email);
public record CreateNotificationEmailResult(NotificationEmail data);

public class CreateNotificationEmailUseCase(INotificationEmailRepository repository) : ICreateNotificationEmailUseCase
{
    public async Task<CreateNotificationEmailResult> Execute(CreateNotificationEmailCommand command)
    {
        NotificationEmail notificationEmail = new()
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
        };

        var createtNotificationEmail = await repository.CreateNotificationEmail(notificationEmail);

        return new CreateNotificationEmailResult(createtNotificationEmail);


    }
}
