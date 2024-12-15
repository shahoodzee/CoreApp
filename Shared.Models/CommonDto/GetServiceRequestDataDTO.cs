using Shared.Models.Contracts;

namespace Shared.Models.CommonDto;

public class GetServiceRequestDataDTO
{
    public long iticketid { get; set; }
    public string ticketnumber { get; set; }
    public string assigneduser { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int iticketstatus { get; set; }
    public string ticketstatus { get; set; }
    public string severity { get; set; }
    public string vtags { get; set; }
    public string notes { get; set; }
    public DateTime createdDate { get; set; }
    public string createdByEmail { get; set; }
    public string createdByName { get; set; }
    public int createdByUserStatus { get; set; }
    public List<Attachment> fileAttachments { get; set; }
    public List<GetSubServicesRequestDataDTO> lstdetail { get; set; }
}