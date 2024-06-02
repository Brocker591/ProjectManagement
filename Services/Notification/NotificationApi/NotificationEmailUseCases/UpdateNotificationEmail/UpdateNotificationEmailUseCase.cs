namespace NotificationApi.NotificationEmailUseCases.UpdateNotificationEmail;

public record UpdateNotificationEmailCommand(NotificationEmail email);
public record UpdateNotificationEmailResult(bool isSuccess);
public class UpdateNotificationEmailUseCase(INotificationEmailRepository repository) : IUpdateNotificationEmailUseCase
{
    public async Task<UpdateNotificationEmailResult> Execute(UpdateNotificationEmailCommand command)
    {
        await repository.UpdateNotificationEmail(command.email);
        return new UpdateNotificationEmailResult(true);
    }
}
