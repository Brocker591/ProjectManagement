using Common.Authorization;
using Common.Authorization.Models;
using System.Security.Claims;


namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoDto(string Desciption, int StatusId, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record ResponseCreateTodo(Todo data);

internal static class CreateTodoEndpoint
{
    public static IEndpointRouteBuilder MapCreateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", async (CreateTodoDto todoDto, ICreateTodoUseCase useCase, IValidator<CreateTodoDto> validator, ClaimsPrincipal user) =>
        {
            //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
            ValidationResult validationResult = await validator.ValidateAsync(todoDto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            IdentifiedUser identifiedUser = user.IdentifyUser();

            if (identifiedUser is null)
                return Results.Unauthorized();


            CreateTodoCommand command = new(
                todoDto.Desciption,
                todoDto.StatusId,
                todoDto.ResponsibleUser ?? identifiedUser.UserId,
                todoDto.EditorUsers ?? new List<Guid> { identifiedUser.UserId },
                todoDto.ProjectId,
                identifiedUser.Tenant ?? TenantConstants.TenantUnknown);



            CreateTodoResult result = await useCase.Execute(command);

            ResponseCreateTodo response = new(result.data);

            return Results.Created($"/tasks/{response.data.Id}", response);
        })
        .WithName("CreateTasks")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Create Tasks")
        .WithDescription("Create Tasks");

        return routes;
    }
}
