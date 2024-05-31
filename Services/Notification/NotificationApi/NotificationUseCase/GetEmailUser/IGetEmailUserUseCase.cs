
namespace NotificationApi.NotificationUseCase.GetEmailUser
{
    public interface IGetEmailUserUseCase
    {
        Task<GetEmailUserResult> Execute(GetEmailUserQuery query);
    }
}