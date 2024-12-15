namespace Shared.Models.Contracts;

public class FetchResponseUserEmailListIds
{
    public long userid { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string? connectionId { get; set; }
    public string? role { get; set; }
    public int userStatus { get; set; } = -1;
}

public class FetchResponseUserEmailListIdsObject
{
    public List<FetchResponseUserEmailListIds> lst { get; set; }
}
