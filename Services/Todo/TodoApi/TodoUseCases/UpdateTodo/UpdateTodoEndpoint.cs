﻿namespace TodoApi.TodoUseCases.UpdateTodo;

public record UpdateTodoDto(Guid Id, string Desciption, int StatusId, Guid? ResponsibleUser, List<Guid>? EditorUsers, bool IsProcessed, Guid? ProjectId);


public static class UpdateTodoEndpoint
{
    public static IEndpointRouteBuilder MapUpdateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/tasks", async (UpdateTodoDto todoDto, IUpdateTodoUseCase useCase, IValidator<UpdateTodoDto> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(todoDto);
                if(!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());


                Todo todo = new()
                {
                    Id = todoDto.Id,
                    Desciption = todoDto.Desciption,
                    StatusId = todoDto.StatusId,
                    ResponsibleUser = todoDto.ResponsibleUser,
                    EditorUsers = todoDto.EditorUsers,
                    ProjectId = todoDto.ProjectId
                };

                UpdateTodoCommand command = new(todo);

                await useCase.Execute(command);

                return Results.NoContent();
            }
            catch (TodoStatusNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (TodoNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception)
            {
                return Results.Problem(detail: "Task could not be updated", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
.WithName("UpdateTasks")
.ProducesProblem(StatusCodes.Status500InternalServerError)
.WithSummary("Update Tasks")
.WithDescription("Update Tasks")
.RequireAuthorization();

        return routes;
    }
}
