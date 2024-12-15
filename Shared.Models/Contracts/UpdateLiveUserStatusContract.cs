namespace Shared.Models.Contracts;

public class UpdateLiveUserStatusContract
{
    public HashSet<long> userIds { get; set; }
}
