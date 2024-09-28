namespace TodoApi.Exceptions;

public class TodoStatusNotFoundException : Exception
{
public TodoStatusNotFoundException(int id) : base($"Status with Id {id} was not found.")
{

}
}