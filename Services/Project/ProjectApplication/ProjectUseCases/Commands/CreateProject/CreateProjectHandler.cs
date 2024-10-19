namespace ProjectApplication.ProjectUseCases.Commands.CreateProject;

public class CreateProjectHandler(IProjectRepositories repository) : ICommandHandler<CreateProjectCommand, CreateProjectResult>
{
    public async Task<CreateProjectResult> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        Project project = Project.Create(
            command.projectDto.Name, 
            command.projectDto.ResponsibleUser, 
            command.projectDto.Tasks, 
            command.projectDto.Users,
            command.userName
            );

        var createdProject = await repository.CreateProject(project);


        return new CreateProjectResult(createdProject);
    }
}
