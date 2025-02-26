namespace MixTJ_Application.Dtos.NewsDto;

public class NewsGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; }
}