namespace Shared.Models;

public class BaseTable
{
    public long iCreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool isActive { get; set; } = true;
}
