namespace ProjectDomain.Events;

public record ProjectClosingEvent(Project project) : IDomainEvent;
