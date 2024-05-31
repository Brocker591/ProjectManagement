namespace NotificationApi.NotificationUseCase.DeleteEmailUser;

public record DeleteEmailUserCommand(Guid id);
public record DeleteEmailUserResult(bool isSuccess);

public class DeleteEmailUserUseCase(IEmailUserRepository repository) : IDeleteEmailUserUseCase
{
    public async Task<DeleteEmailUserResult> Execute(DeleteEmailUserCommand command)
    {
        await repository.DeleteEmailUser(command.id);

        return new DeleteEmailUserResult(true);
    }
}

