namespace NotificationApi.NotificationUseCase.UpdateEmailUser;

public record UpdateEmailUserCommand(EmailUser emailUser);
public record UpdateEmailUserResult(bool isSuccess);
public class UpdateEmailUserUseCase(IEmailUserRepository repository) : IUpdateEmailUserUseCase
{
    public async Task<UpdateEmailUserResult> Execute(UpdateEmailUserCommand command)
    {
        await repository.UpdateEmailUser(command.emailUser);

        return new UpdateEmailUserResult(true);
    }
}
