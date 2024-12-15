namespace Shared.Models.Contracts;

public class CommentAddedEmailContract
{
    public HashSet<string> emails { get; set; }
    public string SRNumber { get; set; }
    public string SRTitle { get; set; }
}
