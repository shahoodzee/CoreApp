using Shared.Models.CommonDto;

namespace Shared.Models.Contracts;

public class ServiceRequestClosedContract
{
    public List<string> connectionIds { get; set; }
    public UnassignedServiceRequestDTO serviceRequestDTO { get; set; }
}
