namespace Shared.Models.Contracts;

public class FetchResponseUser
{
    public long userId { get; set; } = -1;
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public string email { get; set; } = "";
    public string connectionId { get; set; } = "";
    public string phoneNumber { get; set; } = "";
    public string profileImage { get; set; } = "";
    public string companyName { get; set; } = "";
    public int companyId { get; set; } = -1;
    public string role { get; set; } = "";
}
