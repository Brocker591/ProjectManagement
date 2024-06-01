namespace NotificationApi.NotificationUseCase.GetEmailUser;

public record GetEmailUserQuery(Guid id);
public record GetEmailUserResult(EmailUser data);

public class GetEmailUserUseCase(IEmailUserRepository repository) : IGetEmailUserUseCase
{
    public async Task<GetEmailUserResult> Execute(GetEmailUserQuery query)
    {
        var emailUser = await repository.GetEmailUser(query.id);

        return new GetEmailUserResult(emailUser);
    }
}
