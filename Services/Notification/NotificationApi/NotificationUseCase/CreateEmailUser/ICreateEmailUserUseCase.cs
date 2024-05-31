namespace NotificationApi.NotificationUseCase.CreateEmailUser
{
    public interface ICreateEmailUserUseCase
    {
        Task<CreateTodoResult> Execute(CreateEmailUserCommand command);
    }
}