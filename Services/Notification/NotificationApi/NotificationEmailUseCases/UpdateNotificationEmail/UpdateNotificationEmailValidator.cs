namespace NotificationApi.NotificationEmailUseCases.UpdateNotificationEmail
{
    public class UpdateNotificationEmailValidator : AbstractValidator<NotificationEmailDto>
    {
        public UpdateNotificationEmailValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
    }
}
