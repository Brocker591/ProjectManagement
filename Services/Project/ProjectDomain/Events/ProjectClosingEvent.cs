namespace ProjectDomain.Events;

public record ProjectClosingEvent(Guid projectId) : IDomainEvent;
