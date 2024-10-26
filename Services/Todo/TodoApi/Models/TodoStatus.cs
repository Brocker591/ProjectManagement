namespace TodoApi.Models;

public class TodoStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public static string Open = "open";
    public static string Doing = "doing";
    public static string Done = "done";
}