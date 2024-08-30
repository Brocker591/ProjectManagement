namespace ProjectApplication.ProjectUseCases.Queries.GetProjectsFromCurrentUser;

public class GetProjectsFromCurrentUserHandler(IProjectRepositories repository) : IQueryHandler<GetProjectsFromCurrentUserQuery, GetProjectsFromCurrentUserResult>
{
    public async Task<GetProjectsFromCurrentUserResult> Handle(GetProjectsFromCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var projects = await repository.GetProjects();

        var projectsFromCurrentUser = projects.Where(x => x.Users.Contains(query.userId)).ToList();

        return new GetProjectsFromCurrentUserResult(projectsFromCurrentUser);
    }
}
