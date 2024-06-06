namespace Common.MessageEvents;


public record ErrorClosingProjectEvent(string message, ClosingProjectEvent eventObject);