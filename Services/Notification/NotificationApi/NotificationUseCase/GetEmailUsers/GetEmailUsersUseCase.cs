namespace NotificationApi.NotificationUseCase.GetEmailUsers;

public record GetEmailUsersResult(List<EmailUser> data);
public class GetEmailUsersUseCase(IEmailUserRepository repository) : IGetEmailUsersUseCase
{
    public async Task<GetEmailUsersResult> Execute()
    {
        var emailUsers = await repository.GetEmailUsers();

        return new GetEmailUsersResult(emailUsers);
    }
}
