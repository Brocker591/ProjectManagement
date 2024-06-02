namespace NotificationApi.EmailUserUseCases.DeleteEmailUser;

public interface IDeleteEmailUserUseCase
{
    Task<DeleteEmailUserResult> Execute(DeleteEmailUserCommand command);
}