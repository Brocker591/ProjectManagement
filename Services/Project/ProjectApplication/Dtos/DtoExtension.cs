namespace ProjectApplication.Dtos;

public static class DtoExtension
{
    public static ProjectDto AsDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            ResponsibleUser = project.ResponsibleUser,
            Tasks = project.Tasks,
            Users = project.Users,
            IsClosed = project.IsClosed
        };
    }

    public static Project AsModel(this ProjectDto dto)
    {
        return new Project
        {
            Id = dto.Id,
            Name = dto.Name,
            ResponsibleUser = dto.ResponsibleUser,
            Tasks = dto.Tasks,
            Users = dto.Users,
            IsClosed = dto.IsClosed
        };
    }
}
