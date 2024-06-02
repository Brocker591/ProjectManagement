namespace NotificationApi.EmailUserUseCases.UpdateEmailUser
{
    public interface IUpdateEmailUserUseCase
    {
        Task<UpdateEmailUserResult> Execute(UpdateEmailUserCommand command);
    }
}