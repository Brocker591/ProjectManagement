namespace ProjectApplication.ProjectUseCases.Queries.GetProjectsByResponsibleUser;

public record GetProjectsByResponsibleUserQuery(Guid userId) : IQuery<GetProjectsByResponsibleUserResult>;
public record GetProjectsByResponsibleUserResult(List<Project> data);
