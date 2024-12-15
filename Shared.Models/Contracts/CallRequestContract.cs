namespace Shared.Models.Contracts;

public class CallRequestContract
{
    public int Id { get; set; }
    public string CallerName { get; set; }
    public string CallerEmail { get; set; }
    public string CallStatus { get; set; }
    public Dictionary<string, string> Time { get; set; }
}
