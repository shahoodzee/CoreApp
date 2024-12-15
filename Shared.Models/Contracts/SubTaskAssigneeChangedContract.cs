namespace Shared.Models.Contracts;

public class SubTaskAssigneeChangedContract
{
    public long ticketId { get; set; }
    public long Id { get; set; }
    public string DetailTitle { get; set; }
    public string DetailDescription { get; set; }
    public string Status { get; set; }
    public int iStatus { get; set; }
    public string AssignedResources { get; set; }
    public long iAssignedresources { get; set; }
    public double HourDeduction { get; set; }
    public string CreatedByEmail { get; set; }
    public string CustomerConnId { get; set; }
    public string PreviousAssigneeConnId { get; set; }
    public string NewAssigneeConnId { get; set; }
    public string TaskOwnerConnId { get; set; }
}
