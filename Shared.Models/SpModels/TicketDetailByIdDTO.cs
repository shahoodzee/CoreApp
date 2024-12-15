namespace Shared.Models.SpModels;

public class TicketDetailByIdDTO
{
    public long TicketID { get; set; }
    public string ServiceRequestNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public string? Notes { get; set; }
    public string Severity { get; set; }
    public int iStatusId { get; set; }
    public string Status { get; set; }
    public double HourDeduction { get; set; }
    public DateTime CreatedDate { get; set; }
    public long CreatedBy { get; set; }
    public long CreatedByAdmin { get; set; }
    public DateTime ModifyDate { get; set; }
    public long TicketDetailId { get; set; }
    public string DetailTitle { get; set; }
    public string DetailDescription { get; set; }
    public string DetailStatus { get; set; }
    public int iDetailStatusId { get; set; }
    public long iResourceId { get; set; }
    public bool isResourceGroup { get; set; }
    public long iResourceGroupId { get; set; }
    public long detailCreatedBy { get; set; }
}
