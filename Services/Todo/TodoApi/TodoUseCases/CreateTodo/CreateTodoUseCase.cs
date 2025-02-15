﻿using Common.Authorization;
using MassTransit;

namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoCommand(string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, Guid? ProjectId, string Tenant);
public record CreateTodoResult(Todo data);

internal sealed class CreateTodoUseCase(ITodoRepository repository, ITodoStatusRepository statusRepository, IPublishEndpoint publishEndpoint) : ICreateTodoUseCase
{
    public async Task<CreateTodoResult> Execute(CreateTodoCommand command)
    {
        await statusRepository.GetTodoStatus(command.StatusId);



        Todo todo = new()
        {
            Id = Guid.NewGuid(),
            StatusId = command.StatusId,
            Desciption = command.Desciption,
            ResponsibleUser = command.ResponsibleUser,
            EditorUsers = command.EditorUsers,
            ProjectId = command.ProjectId,
            Tenant = command.Tenant ?? TenantConstants.TenantUnknown
        };

        var createdTodo = await repository.CreateTodo(todo);

        if(createdTodo.ProjectId != null &&  createdTodo.ProjectId != Guid.Empty)
        {
            CreateProjectTodoEvent projectTodo = new() { Id = createdTodo.Id, ProjectId = (Guid)createdTodo.ProjectId };
            await publishEndpoint.Publish(projectTodo);
        }
            
        return new CreateTodoResult(createdTodo);
    }
}
