namespace NotificationApi.EmailUserUseCases.GetEmailUsers
{
    public interface IGetEmailUsersUseCase
    {
        Task<GetEmailUsersResult> Execute();
    }
}