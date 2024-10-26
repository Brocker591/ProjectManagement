using System.Security.Claims;

namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoDto(string Desciption, int StatusId, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record ResponseCreateTodo(Todo data);

public static class CreateTodoEndpoint
{
    public static IEndpointRouteBuilder MapCreateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/tasks", async (CreateTodoDto todoDto, ICreateTodoUseCase useCase, IValidator<CreateTodoDto> validator, HttpContext httpContext) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(todoDto);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());


                 Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                if (todoDto.ResponsibleUser is null)
                    todoDto = new CreateTodoDto(todoDto.Desciption, todoDto.StatusId, userId, todoDto.EditorUsers, todoDto.ProjectId);

                if(todoDto.EditorUsers is null)
                    todoDto = new CreateTodoDto(todoDto.Desciption, todoDto.StatusId, todoDto.ResponsibleUser, new List<Guid> { userId }, todoDto.ProjectId);


                var command = todoDto.Adapt<CreateTodoCommand>();




                CreateTodoResult result = await useCase.Execute(command);

                ResponseCreateTodo response = new(result.data);

                return Results.Created($"/tasks/{response.data.Id}", response);
            }
            catch (TodoStatusNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception)
            {
                return Results.Problem(detail: "Task could not be created", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("CreateTasks")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Create Tasks")
        .WithDescription("Create Tasks")
        .RequireAuthorization();

        return routes;
    }
}
