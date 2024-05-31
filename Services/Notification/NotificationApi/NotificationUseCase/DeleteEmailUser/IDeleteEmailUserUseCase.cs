namespace NotificationApi.NotificationUseCase.DeleteEmailUser;

public interface IDeleteEmailUserUseCase
{
    Task<DeleteEmailUserResult> Execute(DeleteEmailUserCommand command);
}