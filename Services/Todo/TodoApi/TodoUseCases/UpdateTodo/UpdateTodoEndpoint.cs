﻿using Common.Authorization;
using Common.Authorization.Models;
using System.Security.Claims;


namespace TodoApi.TodoUseCases.UpdateTodo;

public record UpdateTodoDto(Guid Id, string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, bool IsProcessed, Guid? ProjectId);


public static class UpdateTodoEndpoint
{
    public static IEndpointRouteBuilder MapUpdateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/", async (UpdateTodoDto todoDto, IUpdateTodoUseCase useCase, IValidator<UpdateTodoDto> validator, ClaimsPrincipal user) =>
        {
            //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
            ValidationResult validationResult = await validator.ValidateAsync(todoDto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            IdentifiedUser identifiedUser = user.IdentifyUser();

            if (identifiedUser is null)
                return Results.Unauthorized();

            Todo todo = new()
            {
                Id = todoDto.Id,
                Desciption = todoDto.Desciption,
                StatusId = todoDto.StatusId,
                ResponsibleUser = todoDto.ResponsibleUser,
                EditorUsers = todoDto.EditorUsers,
                ProjectId = todoDto.ProjectId,
                Tenant = identifiedUser.Tenant
            };

            UpdateTodoCommand command = new(todo);

            await useCase.Execute(command);

            return Results.NoContent();

        }).WithName("UpdateTasks")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Update Tasks")
          .WithDescription("Update Tasks");

        return routes;
    }
}
