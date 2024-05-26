namespace ProjectApplication.ProjectUseCases.Queries.GetProjectsByResponsibleUser;

public class GetProjectsByResponsibleUserHandler(IProjectRepositories repository) : IQueryHandler<GetProjectsByResponsibleUserQuery, GetProjectsByResponsibleUserResult>
{
    public async Task<GetProjectsByResponsibleUserResult> Handle(GetProjectsByResponsibleUserQuery query, CancellationToken cancellationToken)
    {
        var allProjects = await repository.GetProjects();

        var projectsByResponsibleUser = allProjects.Where(x => x.ResponsibleUser == query.userId).ToList();

        return new GetProjectsByResponsibleUserResult(projectsByResponsibleUser);
    }
}
