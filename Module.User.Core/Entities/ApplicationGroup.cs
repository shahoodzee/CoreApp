using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationGroup
{
    [Key]
    public int Id { get; set; }
    public string GroupName { get; set; }
    public string Description { get; set; }
    public bool isActive { get; set; }
    public long iCreatedBy { get; set; } = -1;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
