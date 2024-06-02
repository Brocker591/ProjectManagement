namespace NotificationApi.NotificationEmailUseCases.DeleteNotificationEmail;

public record DeleteNotificationEmailCommand(Guid id);
public record DeleteNotificationEmailResult(bool isSuccess);
public class DeleteNotificationEmailUseCase(INotificationEmailRepository repository) : IDeleteNotificationEmailUseCase
{
    public async Task<DeleteNotificationEmailResult> Execute(DeleteNotificationEmailCommand command)
    {
        await repository.DeleteNotificationEmail(command.id);
        return new DeleteNotificationEmailResult(true);
    }
}
