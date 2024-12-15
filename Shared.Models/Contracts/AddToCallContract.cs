namespace Shared.Models.Contracts;

public class AddToCallContract
{
    public string ReceiverToken { get; set; }
    public string ReceiverConnectionId { get; set; }
    public string MeetingId { get; set; }
    public string CallerEmail { get; set; }
    public long CallRequestId { get; set; }
}
