namespace Shared.Models.Contracts;

public class FetchResponseSRAssociatedUsers
{
    public List<UsersDto> users { get; set; }
    public string ticketNumber { get; set; }
    public string ticketTitle { get; set; }
}

public class UsersDto
{
    public long userId { get; set; } = -1;
    public string userName { get; set; } = "";
    public string connectionId { get; set; } = "";
    public string email { get; set; } = "";
    public long statusId { get; set; } = -1;
}