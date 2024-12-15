namespace Shared.Models.Contracts;

public class CommentAddedContract
{
    public HashSet<string> connectionIds { get; set; }
    public string CreatedBy { get; set; }
    public string ServiceRequestNumber { get; set; }
    public long ServiceRequestId { get; set; }
}
