namespace Shared.Models.Contracts;

public class FetchResponseAttachmentsContract
{
    public List<Attachment> Files { get; set; }
}

public class Attachment
{
    public string FileUrl { get; set; }
    public string FileName { get; set; }
}
