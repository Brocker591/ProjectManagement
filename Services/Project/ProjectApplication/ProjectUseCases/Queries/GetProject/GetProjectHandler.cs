
namespace ProjectApplication.ProjectUseCases.Queries.GetProject;

public class GetProjectHandler(IProjectRepositories repository) : IQueryHandler<GetProjectQuery, GetProjectResult>
{
    public async Task<GetProjectResult> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        var project = await repository.GetProject(query.id);

        return new GetProjectResult(project);
    }
}
