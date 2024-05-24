using Microsoft.AspNetCore.Mvc;

namespace TodoApi.TodoUseCases.UpdateTodo;

public class UpdateTodoDto
{
    [Required]
    public Guid Id { get; init; }
    [Required] 
    public string Desciption { get; init; }
    public Guid? ResponsibleUser { get; init; }
    public List<Guid>? EditorUsers { get; init; }
    [Required] 
    public bool IsProcessed { get; init; }
    public Guid? ProjectId { get; init; }
};



public static class UpdateTodoEndpoint
{
    public static IEndpointRouteBuilder MapUpdateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/tasks", async ([FromBody] UpdateTodoDto todoDto, IUpdateTodoUseCase useCase) =>
        {
            try
            {
                Todo todo = new()
                {
                    Id = todoDto.Id,
                    Desciption = todoDto.Desciption,
                    ResponsibleUser = todoDto.ResponsibleUser,
                    EditorUsers = todoDto.EditorUsers,
                    IsProcessed = todoDto.IsProcessed,
                    ProjectId = todoDto.ProjectId
                };

                UpdateTodoCommand command = new(todo);

                await useCase.Execute(command);

                return Results.NoContent();
            }
            catch (TodoNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Task could not be updated", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
.WithName("UpdateTasks")
.ProducesProblem(StatusCodes.Status500InternalServerError)
.WithSummary("Update Tasks")
.WithDescription("Update Tasks");

        return routes;
    }
}
