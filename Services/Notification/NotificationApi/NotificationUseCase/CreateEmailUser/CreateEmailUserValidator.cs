namespace NotificationApi.NotificationUseCase.CreateEmailUser;

public class CreateEmailUserValidator : AbstractValidator<CreateEmailUserDto>
{
    public CreateEmailUserValidator()
    {
        RuleFor(x => x.Smtp_Username).NotNull().MinimumLength(4);
        RuleFor(x => x.Smtp_Password).NotNull().MinimumLength(4);
        RuleFor(x => x.Host).NotNull().MinimumLength(4);
    }
}
