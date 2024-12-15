namespace Shared.Models.Contracts;

public class CallAcceptContract
{
    public string CallerToken { get; set; }
    public string ReceiverToken { get; set; }
    public string CallerConnectionId { get; set; }
    public string ReceiverConnectionId { get; set; }
    public string MeetingId { get; set; }
    public string CallerEmail { get; set; }
    public long CallRequestId { get; set; }
}
