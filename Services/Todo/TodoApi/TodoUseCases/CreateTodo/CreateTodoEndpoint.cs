namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoDto(string Desciption, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record ResponseCreateTodo(Todo data);

public static class CreateTodoEndpoint
{
    public static IEndpointRouteBuilder MapCreateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/tasks", async (CreateTodoDto todoDto, ICreateTodoUseCase useCase, IValidator<CreateTodoDto> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(todoDto);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());


                var command = todoDto.Adapt<CreateTodoCommand>();

                CreateTodoResult result = await useCase.Execute(command);

                ResponseCreateTodo response = new(result.data);

                return Results.Created($"/tasks/{response.data.Id}", response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Task could not be created", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("CreateTasks")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Create Tasks")
        .WithDescription("Create Tasks");

        return routes;
    }
}
