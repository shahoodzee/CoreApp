namespace Shared.Models.Contracts;

public class SubTaskApprovalContract
{
    public long ticketId { get; set; }
    public long id { get; set; }
    public string detailtitle { get; set; }
    public string detaildescription { get; set; }
    public string status { get; set; }
    public int istatus { get; set; }
    public string assignedresources { get; set; }
    public long iassignedresources { get; set; }
    public double hoursdeduction { get; set; }
    public string createdByEmail { get; set; }
    public string subtaskCreatedByConnId { get; set; }
    public string subtaskAssigneeConnId { get; set; }
}
