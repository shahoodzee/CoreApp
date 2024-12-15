namespace Shared.Models.Contracts;

public class FetchResponseUsersByRole
{
    public List<string> connectionIds { get; set; }
    public List<string> emails { get; set; }
    public List<long> userIds { get; set; }
}
