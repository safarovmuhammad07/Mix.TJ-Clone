namespace MixTJ_Application.Dtos.FileDto;

public class UploadedFileGetDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}