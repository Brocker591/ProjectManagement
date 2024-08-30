namespace ProjectApplication.ProjectUseCases.Queries.GetProjectsFromCurrentUser;

public record GetProjectsFromCurrentUserQuery(Guid userId) : IQuery<GetProjectsFromCurrentUserResult>;
public record GetProjectsFromCurrentUserResult(List<Project> data);


