namespace TodoApi.TodoUseCases.CreateTodo;

internal sealed class CreateTodoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Desciption).NotNull().MinimumLength(5);
    }
}
