namespace Common.MessageEvents;

public record ErrorDeleteProjectEvent(string message, DeleteProjectEvent eventObject);
