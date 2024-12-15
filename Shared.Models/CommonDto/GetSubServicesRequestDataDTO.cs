namespace Shared.Models.CommonDto;

public class GetSubServicesRequestDataDTO
{
    public long id { get; set; }
    public string detailtitle { get; set; }
    public string detaildescription { get; set; }
    public string status { get; set; }
    public int istatus { get; set; }
    public string assignedresources { get; set; }
    public long iassignedresources { get; set; }
    public double hoursdeduction { get; set; }
    public string createdByEmail { get; set; }
}
