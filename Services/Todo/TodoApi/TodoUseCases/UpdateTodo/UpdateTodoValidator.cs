namespace TodoApi.TodoUseCases.UpdateTodo;

public class UpdateTodoValidator : AbstractValidator<UpdateTodoDto>
{
    public UpdateTodoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Desciption).NotNull().MinimumLength(5);
        RuleFor(x => x.IsProcessed).NotNull();
        RuleFor(x => x.ResponsibleUser).NotNull();
        RuleFor(x => x.EditorUsers).NotNull();
    }


}
