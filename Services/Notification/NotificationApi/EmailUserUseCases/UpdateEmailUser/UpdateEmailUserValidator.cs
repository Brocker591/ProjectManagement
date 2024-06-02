namespace NotificationApi.EmailUserUseCases.UpdateEmailUser;

public class UpdateEmailUserValidator : AbstractValidator<EmailUser>
{
    public UpdateEmailUserValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Smtp_Username).NotNull().MinimumLength(4);
        RuleFor(x => x.Smtp_Password).NotNull().MinimumLength(4);
        RuleFor(x => x.Host).NotNull().MinimumLength(4);
    }
}

