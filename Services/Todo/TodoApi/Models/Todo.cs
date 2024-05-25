namespace TodoApi.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Desciption { get; set; } = default!;
        public Guid? ResponsibleUser { get; set; }
        public List<Guid>? EditorUsers { get; set; }
        public bool IsProcessed { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
