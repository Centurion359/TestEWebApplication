namespace TestЕWebApplication1.Models;

public class Task
{
    public Task(string title, string summary, byte image)
    {
        CreatedAt = DateTime.UtcNow;
        Title = title;
        Summary = summary;
        Image = image;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public byte Image { get; set; }
}