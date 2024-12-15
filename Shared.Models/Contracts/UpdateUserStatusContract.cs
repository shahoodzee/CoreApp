namespace Shared.Models.Contracts;

public class UpdateUserStatusContract
{
    public HashSet<long> userIds { get; set; }
    public int statusId { get; set; }
}
