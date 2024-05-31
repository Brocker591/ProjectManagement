namespace NotificationApi.NotificationUseCase.CreateEmailUser;

public record CreateEmailUserCommand(string Smtp_Username, string Smtp_Password, string Host, int Port, bool EnableSsl);
public record CreateTodoResult(EmailUser data);
public class CreateEmailUserUseCase(IEmailUserRepository repository) : ICreateEmailUserUseCase
{
    public async Task<CreateTodoResult> Execute(CreateEmailUserCommand command)
    {
        EmailUser emailUser = new()
        {
            Id = Guid.NewGuid(),
            Smtp_Username = command.Smtp_Username,
            Smtp_Password = command.Smtp_Password,
            Host = command.Host,
            Port = command.Port,
            EnableSsl = command.EnableSsl,
        };

        var createdEmailUser = await repository.CreateEmailUser(emailUser);

        return new CreateTodoResult(createdEmailUser);
    }
}
