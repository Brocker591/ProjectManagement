namespace ProjectApplication.ProjectUseCases.Queries.GetProjects;

public class GetProjectsHandler(IProjectRepositories repository) : IQueryHandler<GetProjectsQuery, GetProjectsResult>
{
    public async Task<GetProjectsResult> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await repository.GetProjects();

        return new GetProjectsResult(projects);
    }
}
