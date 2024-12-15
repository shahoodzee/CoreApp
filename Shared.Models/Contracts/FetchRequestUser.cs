namespace Shared.Models.Contracts;

public class FetchRequestUser
{
    public string email { get; set; }
    public string connectionId { get; set; }
    public long userId { get; set; } = -1;
}
