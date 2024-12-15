namespace Shared.Models.Contracts;

public class SubTaskResolvedEmailContract
{
    public List<string> emails { get; set; }
    public string SRNumber { get; set; }
    public string SRTitle { get; set; }
    public string SubTaskTitle { get; set; }
}
