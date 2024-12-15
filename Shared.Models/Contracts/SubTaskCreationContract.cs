namespace Shared.Models.Contracts;

public class SubTaskCreationContract
{
    public long ticketId { get; set; }
    public long Id { get; set; }
    public string DetailTitle { get; set; }
    public string DetailDescription { get; set; }
    public string Status { get; set; }
    public int iStatus { get; set; }
    public long iAssignedresources { get; set; }
    public double HourDeduction { get; set; }
    public long iCustomer { get; set; }
    public string CreatedByEmail { get; set; }
}
