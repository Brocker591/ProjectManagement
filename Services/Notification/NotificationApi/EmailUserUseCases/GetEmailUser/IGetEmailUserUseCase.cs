namespace NotificationApi.EmailUserUseCases.GetEmailUser
{
    public interface IGetEmailUserUseCase
    {
        Task<GetEmailUserResult> Execute(GetEmailUserQuery query);
    }
}