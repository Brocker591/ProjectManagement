
namespace NotificationApi.NotificationUseCase.UpdateEmailUser
{
    public interface IUpdateEmailUserUseCase
    {
        Task<UpdateEmailUserResult> Execute(UpdateEmailUserCommand command);
    }
}