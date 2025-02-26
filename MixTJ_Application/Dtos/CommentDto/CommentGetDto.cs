namespace MixTJ_Application.Dtos.CommentDto;

public class CommentGetDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int? ParentCommentId { get; set; }
}