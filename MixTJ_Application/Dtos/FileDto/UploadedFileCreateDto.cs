namespace MixTJ_Application.Dtos.FileDto;

public class UploadedFileCreateDto
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}