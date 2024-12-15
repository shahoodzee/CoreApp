namespace Shared.Models.Contracts;

public class FetchRequestPackage
{
    public int iPackageId { get; set; }
    public int itierId { get; set; } = -1;
    public bool isYearly { get; set; } = false;
}
