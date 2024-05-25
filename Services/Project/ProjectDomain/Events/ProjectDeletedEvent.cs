namespace ProjectDomain.Events;

public record ProjectDeletedEvent(Project project) : IDomainEvent;
