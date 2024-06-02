namespace NotificationApi.NotificationEmailUseCases.CreateNotificationEmail;

public class CreateNotificationEmailValidator : AbstractValidator<CreateNotificationEmailDto>
{
    public CreateNotificationEmailValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress();
    }
}
