namespace TodoApi.TodoUseCases.CreateTodo;

public class CreateTodoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Desciption).NotNull().MinimumLength(5);
    }
}
