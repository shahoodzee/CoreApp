using Shared.Models.CommonDto;

namespace Shared.Models.Contracts;

public class ServiceRequestInReviewContract
{
    public List<string> connectionIds { get; set; }
    public UnassignedServiceRequestDTO serviceRequestDTO { get; set; }
}
