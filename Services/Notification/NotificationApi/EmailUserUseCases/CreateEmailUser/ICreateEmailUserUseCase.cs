namespace NotificationApi.EmailUserUseCases.CreateEmailUser
{
    public interface ICreateEmailUserUseCase
    {
        Task<CreateTodoResult> Execute(CreateEmailUserCommand command);
    }
}