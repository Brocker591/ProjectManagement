﻿namespace TodoApi.TodoUseCases.GetTodos;

public record GetTodosResult(List<Todo> data);
public class GetTodosUseCase(ITodoRepository repository) : IGetTodosUseCase
{
    public async Task<GetTodosResult> Execute()
    {
        var todos = await repository.GetTodos();

        return new GetTodosResult(todos);
    }
}
