using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoDto(string Desciption, int StatusId, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record ResponseCreateTodo(Todo data);

public static class CreateTodoEndpoint
{
    public static IEndpointRouteBuilder MapCreateTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", async (CreateTodoDto todoDto, ICreateTodoUseCase useCase, IValidator<CreateTodoDto> validator, ClaimsPrincipal user) =>
        {
            //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
            ValidationResult validationResult = await validator.ValidateAsync(todoDto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var userIdString = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

            Guid userId;

            if (!Guid.TryParse(userIdString, out userId))
            {
                return Results.Unauthorized();

            }

            string? tenant = user.FindFirstValue(Tenant.TenantName);

            if (todoDto.ResponsibleUser is null)
                todoDto = new CreateTodoDto(todoDto.Desciption, todoDto.StatusId, userId, todoDto.EditorUsers, todoDto.ProjectId);

            if (todoDto.EditorUsers is null)
                todoDto = new CreateTodoDto(todoDto.Desciption, todoDto.StatusId, todoDto.ResponsibleUser, new List<Guid> { userId }, todoDto.ProjectId);

            CreateTodoCommand command = new(
                todoDto.Desciption,
                todoDto.StatusId,
                todoDto.ResponsibleUser ?? userId,
                todoDto.EditorUsers ?? new List<Guid> { userId },
                todoDto.ProjectId,
                tenant ?? Tenant.TenantUnknown);



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
