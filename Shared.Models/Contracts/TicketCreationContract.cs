namespace Shared.Models.Contracts;

public class TicketCreationContract
{

    public long TicketId { get; set; }
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public string Notes { get; set; }
    public int iTicketStatus { get; set; }
    public string TicketStatus { get; set; }
    public string Priority { get; set; }
    public string CreatedBy { get; set; }
    public long iCreatedBy { get; set; }
    public string CreatedFor { get; set; }
    public long iCreatedFor { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool isCreatedByAdmin { get; set; }
}
