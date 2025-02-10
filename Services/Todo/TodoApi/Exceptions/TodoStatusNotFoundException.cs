namespace TodoApi.Exceptions;

public class TodoStatusNotFoundException : Exception
{
public TodoStatusNotFoundException(int id) : base($"Task status with Id {id} was not found.")
{

}
}