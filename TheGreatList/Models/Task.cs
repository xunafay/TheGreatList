namespace TheGreatList.Models;

public class Task
{
    public DateOnly CreationDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Completed { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
