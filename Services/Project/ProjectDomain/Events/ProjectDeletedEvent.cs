using MediatR;

namespace ProjectDomain.Events;

public record ProjectDeletedEvent(Guid projectId) : IDomainEvent;
